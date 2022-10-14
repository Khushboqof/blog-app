using BlogApp.Api.ViewModels.Users;

namespace BlogApp.Api.Inerfaces.Services
{
    public interface IAccountService
    {
        Task<string?> LogInAsync(UserLoginViewModel viewModel);

        Task<bool> RegistrAsync(UserCreateViewModel viewModel);

        Task<bool> VerifyEmail(EmailVerify emailVerify);

        Task SendCodeAsync(SendToEmail email);

        Task<bool> VerifyPasswordAsync(UserResetPasswordViewModel password);

    }
}
