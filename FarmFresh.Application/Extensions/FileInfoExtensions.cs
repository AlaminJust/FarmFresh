using Microsoft.Extensions.FileProviders;

namespace FarmFresh.Application.Extensions
{
    public static class FileInfoExtensions
    {
        public static async Task WriteTextAsync(this IFileInfo fileInfo, string text)
        {
            using (var writer = File.CreateText(fileInfo.PhysicalPath))
            {
                await writer.WriteAsync(text);
            }
        }
    }
}
