using BlogApp.Api.Entities;

namespace BlogApp.Api.ViewModels.Users
{
    public class UserViewModel
    {
        public long Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsEmailConfirmed { get; set; } = false;

        public string ImageUrl { get; set; } = string.Empty;

        public static implicit operator UserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ImageUrl = user.ImagePath
            };
        }
    }
}