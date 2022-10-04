namespace BlogApp.Api.Inerfaces.Services
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile file);
    }
}
