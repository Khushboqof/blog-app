using BlogApp.Api.Commons.Attributes;
using BlogApp.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Api.ViewModels.Users
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "FirstName is required")]
        [MaxLength(50), MinLength(2)]
        [RegularExpression(@"^(?=.{1,40}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$",
            ErrorMessage = "Please enter valid first name. " +
            "First name must be contains only letters or ' character")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50), MinLength(2)]
        [RegularExpression(@"^(?=.{1,40}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$",
            ErrorMessage = "Please enter valid last name. " +
            "last name must be contains only letters or ' character")]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(50), MinLength(2)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter valid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StrongPassword]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image is required")]
        [DataType(DataType.Upload)]
        [MaxFileSize(3)]
        [AllowedFileExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Image { get; set; } = null!;

        public static implicit operator User(UserCreateViewModel userCreateView)
        {
            return new User
            {
                FirstName = userCreateView.FirstName,
                LastName = userCreateView.LastName,
                Email = userCreateView.Email,
                PasswordHash = userCreateView.Password,
            };
        }
    }
}
