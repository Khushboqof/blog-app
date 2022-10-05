using BlogApp.Api.ViewModels.Users;

namespace BlogApp.Api.Inerfaces.Services
{
    public interface IEmailService
    {
        public Task SendAsync(EmailMesage mesage);
    }
}
