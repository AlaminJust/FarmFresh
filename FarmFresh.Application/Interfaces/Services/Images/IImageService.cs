using Microsoft.AspNetCore.Http;

namespace FarmFresh.Application.Interfaces.Services.Images
{
    public interface IImageService
    {
        Task<string> SaveToFolderAsync(IFormFile files, string fileUrl);
    }
}
