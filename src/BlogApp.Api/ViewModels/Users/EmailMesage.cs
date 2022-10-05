using System.ComponentModel.DataAnnotations;

namespace BlogApp.Api.ViewModels.Users
{
    public class EmailMesage
    {
        [Required]
        public string To { get; set; } = string.Empty;

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        public int Body { get; set; }

        public bool IsEmailConfirmed { get; set; }
    }
}
