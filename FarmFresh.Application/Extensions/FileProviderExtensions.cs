using Microsoft.Extensions.FileProviders;

namespace FarmFresh.Application.Extensions
{
    public static class FileProviderExtensions
    {
        public static async Task<IFileInfo> WriteTextToFileAsync(this IFileProvider fileProvider, string fileName,
            string text)
        {
            IFileInfo fileInfo = fileProvider.GetFileInfo(fileName);
            if (fileInfo.IsDirectory)
            {
                throw new InvalidOperationException("Only file info supported.");
            }

            string dir = Path.GetDirectoryName(fileInfo.PhysicalPath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            await fileInfo.WriteTextAsync(text);

            return fileInfo;
        }

        public static IFileInfo GetOrCreateDirInfo(this IFileProvider fileProvider, string subpath)
        {
            if (subpath.IsMissing())
            {
                throw new ArgumentNullException(nameof(subpath));
            }

            IFileInfo fileInfo = fileProvider.GetFileInfo(subpath);
            if (!fileInfo.Exists)
            {
                Directory.CreateDirectory(fileInfo.PhysicalPath);
            }

            return fileInfo;
        }
    }
}
