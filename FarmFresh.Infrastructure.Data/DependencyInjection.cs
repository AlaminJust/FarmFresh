using FarmFresh.Domain.Contexts;
using FarmFresh.Domain.Repo;
using FarmFresh.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            string connectionString)
        {

            services.AddDbContext<EFDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(EFDbContext).Assembly.FullName)), ServiceLifetime.Scoped);

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<EFDbContext>());
            services.AddScoped<ITransactionUtil, TransactionUtil>();
            return services;
        }
    }
}
