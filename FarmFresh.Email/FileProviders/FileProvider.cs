using Microsoft.Extensions.FileProviders;
using System.Text;

namespace Taap.Email.FileProviders
{
    public class FileProvider
    {
        private readonly string folderPath;

        public FileProvider(string folderPath)
        {
            this.folderPath = folderPath;
        }
        private Task<string> GetDirectoryAsync()
        {
            return Task.FromResult(String.Concat(Environment.CurrentDirectory, this.folderPath));
        }

        private async Task<IFileProvider> GetFileProviderAsync()
        {
            return new PhysicalFileProvider(await this.GetDirectoryAsync());
        }

        public async Task<StreamReader> GetReadStreamAsync(string fileName)
        {
            IFileProvider provider = await GetFileProviderAsync();
            var files = provider.GetFileInfo(fileName);
            return new StreamReader(provider.GetFileInfo($"\\{fileName}").CreateReadStream(),
                Encoding.UTF8);
        }

        public async Task<string> ReadFileToEndAsync(string fileName)
        {
            using (StreamReader reader = await GetReadStreamAsync(fileName))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
