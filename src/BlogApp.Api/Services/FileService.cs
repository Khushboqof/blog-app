using BlogApp.Api.Commons.Helpers;
using BlogApp.Api.Inerfaces.Services;

namespace BlogApp.Api.Services
{
    public class FileService : IFileService
    {
        private readonly string _basePath = string.Empty;
        private readonly string _imageFolderName = "images";

        public FileService(IWebHostEnvironment webHost)
        {
            _basePath = webHost.WebRootPath;
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            string fileName = ImageHelper.MakeImageName(file.FileName);
            string partPath = Path.Combine(_imageFolderName, fileName);
            string path = Path.Combine(_basePath, partPath);

            var stream = File.Create(path);
            await file.CopyToAsync(stream);
            return partPath;

        }
    }
}
