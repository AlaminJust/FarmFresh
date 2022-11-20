using FarmFresh.Domain.Unity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace TAAP.Domain.Unity
{
    public class Enviroment : IEnviroment
    {
        public bool IsDevelopment { get; set; }
        public IFileProvider EmailFileProvider { get; set; }

        [Obsolete]
        public Enviroment(
                IHostingEnvironment hostingEnvironment,
                IConfiguration configuration
            ) 
        {
            IsDevelopment = hostingEnvironment.IsDevelopment();
        }
    }
}
