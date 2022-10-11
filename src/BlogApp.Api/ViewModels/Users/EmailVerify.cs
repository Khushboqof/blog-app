using System.ComponentModel.DataAnnotations;

namespace BlogApp.Api.ViewModels.Users
{
    public class EmailVerify
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter valid email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int Code { get; set; }
    }
}