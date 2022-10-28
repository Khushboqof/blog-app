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
            if (!Directory.Exists(_basePath))
            {
                Directory.CreateDirectory(_basePath);
            }
            string imagepath = Path.Combine(_basePath, _imageFolderName);
            if (!Directory.Exists(imagepath))
            {
                Directory.CreateDirectory(imagepath);
            }
        }

        ///<summary>
        ///this method th file save
        ///<summary>
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            string fileName = ImageHelper.MakeImageName(file.FileName);
            string partPath = Path.Combine(_imageFolderName, fileName);
            string path = Path.Combine(_basePath, partPath);

            var stream = File.Create(path);
            await file.CopyToAsync(stream);
            stream.Close();

            return partPath;
        }

        public Task<bool> DeleteImageAsync(string relativeImagePath)
        {
            string absoluteFilePath = Path.Combine(_basePath, relativeImagePath);

            if(!File.Exists(absoluteFilePath)) return Task.FromResult(false);

            try
            {
                File.Delete(absoluteFilePath);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);  
            }
        }
    }
}