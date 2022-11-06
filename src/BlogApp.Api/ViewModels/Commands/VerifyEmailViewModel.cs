using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace RaqamliAvlod.Application.ViewModels.Accounts.Commands
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Email is required"), Email]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int Code { get; set; }
    }
}
