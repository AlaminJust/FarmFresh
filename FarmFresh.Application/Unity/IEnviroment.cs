
using Microsoft.Extensions.FileProviders;

namespace FarmFresh.Domain.Unity
{
    public interface IEnviroment
    {
        Boolean IsDevelopment { get; }
        IFileProvider EmailFileProvider { get; }
    }
}
