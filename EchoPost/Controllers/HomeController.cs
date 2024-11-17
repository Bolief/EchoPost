using EchoPost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EchoPost.Controllers
{
    public class HomeController : Controller
    {
        private readonly EchoPostDbContext _context;

        public HomeController(EchoPostDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var recentPosts = await _context.Posts
                .OrderByDescending(p => p.CreatedAt)
                .Include(p => p.Forum)
                .Take(10)
                .ToListAsync();

            var popularForums = await _context.Forums
                .OrderByDescending(f => f.Posts.Count)
                .Take(5)
                .ToListAsync();

            var model = new HomeViewModel
            {
                RecentPosts = recentPosts,
                PopularForums = popularForums
            };

            return View(model);
        }
    }


}