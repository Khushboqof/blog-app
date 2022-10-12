using BlogApp.Api.Commons.Attributes;
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

        [Required(ErrorMessage = "Image is required")]
        [DataType(DataType.Upload)]
        [MaxFileSize(3)]
        [AllowedFileExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Image { get; set; } = null!;

        public static implicit operator BlogPost(BlogCreateViewModel viewModel)
        {
            return new BlogPost
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
            };
        }
    }
}
