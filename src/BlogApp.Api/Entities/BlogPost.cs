namespace BlogApp.Api.Entities
{
    public class BlogPost
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public long ViewCount { get; set; } = 0;

        public string ImagePath { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public long UserId { get; set; }

        public virtual User? User { get; set; } = null!;
    }
}
