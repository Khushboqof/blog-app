using BlogApp.Api.Commons.Attributes;
using BlogApp.Api.Entities;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace RaqamliAvlod.Application.ViewModels.Accounts.Commands
{
    public class AccountCreateViewModel
    {
        [Required, MinLength(3)]
        public string Firstname { get; set; } = string.Empty;
        [Required, MinLength(3)]
        public string Lastname { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, Email]
        public string Email { get; set; } = string.Empty;
        [Required, StrongPassword]
        public string Password { get; set; } = string.Empty;

        public static implicit operator User(AccountCreateViewModel accountCreateViewModel)
        {
            return new User()
            {
                FirstName = accountCreateViewModel.Firstname,
                LastName = accountCreateViewModel.Lastname,
                PhoneNumber = accountCreateViewModel.PhoneNumber,
                Email = accountCreateViewModel.Email,
            };
        }
    }
}
