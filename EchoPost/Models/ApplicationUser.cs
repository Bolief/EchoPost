using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace EchoPost.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
