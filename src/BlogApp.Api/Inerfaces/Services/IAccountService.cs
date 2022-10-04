using BlogApp.Api.ViewModels.Users;

namespace BlogApp.Api.Inerfaces.Services
{
    public interface IAccountService
    {
        Task<string> LogInAsync(UserLoginViewModel viewModel);

        Task<bool> RegistrAsync(UserCreateViewModel viewModel);
    }
}
