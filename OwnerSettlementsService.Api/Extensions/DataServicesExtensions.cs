using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using OwnerSettlementsService.Data;

namespace OwnerSettlementsService.Api.Extensions
{
    public static class DataServicesExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OSSDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("OwnerSettlementsService.Data")));
        }
    }
}
