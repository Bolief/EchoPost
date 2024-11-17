namespace EchoPost.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        // Foreign keys and navigation properties
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ForumId { get; set; }
        public Forum Forum { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
