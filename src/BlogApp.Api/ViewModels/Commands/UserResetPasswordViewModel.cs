using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace RaqamliAvlod.Application.ViewModels.Accounts.Commands
{
    public class UserResetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required"), Email]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int Code { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
