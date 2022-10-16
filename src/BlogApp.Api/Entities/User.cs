using BlogApp.Api.Enums;
using BlogApp.Api.ViewModels.Users;

namespace BlogApp.Api.Entities
{
    public class User
    {
        public long Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public bool IsEmailConfirmed { get; set; } = false;

        public string ImagePath { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;

        public UserRole UserRole { get; set; } = UserRole.User;

        public virtual ICollection<BlogPost> BlogPosts { get; set; }
    }
}
