using FarmFresh.Application.Interfaces.Services.Images;
using Microsoft.AspNetCore.Http;

namespace FarmFresh.Infrastructure.Service.Services.Images
{
    public class ImageService : IImageService
    {
        private string generateFileUrl(string fileName)
        {
            string fileUrl;
            while (true)
            {
                fileUrl = Guid.NewGuid().ToString() + fileName;
                if (!Directory.Exists(fileUrl))
                    break;
            }
            return fileUrl;
        }

        public async Task<string> SaveToFolderAsync(IFormFile files, string fileUrl)
        {
            var fileName = files.FileName;
            var uniquePath = generateFileUrl(fileName);
            var imageFileFullPath = Path.Combine(fileUrl, uniquePath);
            using (var fileStream = new FileStream(imageFileFullPath, FileMode.Create))
            {
                await files.CopyToAsync(fileStream);
            }
            return uniquePath;
        }
    }
}
