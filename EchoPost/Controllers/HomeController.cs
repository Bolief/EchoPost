using EchoPost.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EchoPost.Controllers
{
    public class HomeController : Controller
    {
        private readonly EchoPostDbContext _context;

        public HomeController(EchoPostDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? numberOfPosts, int? numberOfForums)
        {
            // Set default values if parameters are not provided
            int postsToFetch = numberOfPosts ?? 10; // Fetch 10 posts by default
            int forumsToFetch = numberOfForums ?? 5; // Fetch 5 forums by default

            try
            {
                var recentPosts = await _context.Posts
                    .OrderByDescending(p => p.CreatedAt)
                    .Include(p => p.Forum)
                    .Take(postsToFetch)
                    .ToListAsync();

                var popularForums = await _context.Forums
                    .OrderByDescending(f => f.Posts.Count)
                    .Take(forumsToFetch)
                    .ToListAsync();

                var model = new HomeViewModel
                {
                    RecentPosts = recentPosts,
                    PopularForums = popularForums
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Log error and return a friendly error view
                Debug.WriteLine($"Error fetching data: {ex.Message}");
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}
