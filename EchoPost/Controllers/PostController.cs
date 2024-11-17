using EchoPost.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EchoPost.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly EchoPostDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostController(EchoPostDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            var user = await _userManager.GetUserAsync(User);
            post.UserId = user.Id;
            post.CreatedAt = DateTime.Now;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Forum", new { id = post.ForumId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Comment(Comment comment)
        {
            var user = await _userManager.GetUserAsync(User);
            comment.UserId = user.Id;
            comment.CreatedAt = DateTime.Now;

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = comment.PostId });
        }
    }

}
