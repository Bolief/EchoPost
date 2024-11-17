using EchoPost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace EchoPost.Controllers
{
    public class ForumController : Controller
    {
        private readonly EchoPostDbContext _context;

        public ForumController(EchoPostDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var forums = await _context.Forums.ToListAsync();
            return View(forums);
        }

        public async Task<IActionResult> Details(int id, string sortOrder)
        {
            var forum = await _context.Forums
                .Include(f => f.Posts)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (sortOrder == "date")
            {
                forum.Posts = forum.Posts.OrderByDescending(p => p.CreatedAt).ToList();
            }

            return View(forum);
        }
    }

}
