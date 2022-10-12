using BlogApp.Api.Entities;

namespace BlogApp.Api.ViewModels.Blogs
{
    public class BlogViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public long ViewCount { get; set; } = 0;

        public string ImageUrl { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Username { get; set; } = string.Empty;

        public static implicit operator BlogViewModel(BlogPost blog)
        {
            return new BlogViewModel
            {
                Title = blog.Title,
                Description = blog.Description,
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt,
                ViewCount = blog.ViewCount,
                ImageUrl = blog.ImagePath
            };
        }
    }
}
