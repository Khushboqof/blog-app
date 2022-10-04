using BlogApp.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Api.ViewModels.Blogs
{
    public class BlogCreateViewModel
    {
        [Required, MaxLength(50), MinLength(2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        public static implicit operator BlogPost(BlogCreateViewModel viewModel)
        {
            return new BlogPost
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                UserId = viewModel.UserId,
            };
        }
    }
}
