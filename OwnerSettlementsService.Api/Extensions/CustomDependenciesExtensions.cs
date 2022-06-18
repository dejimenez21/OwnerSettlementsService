using Microsoft.Extensions.DependencyInjection;
using OwnerSettlementsService.Core.Services;
using OwnerSettlementsService.Core.Services.Abstractions;
using OwnerSettlementsService.Data.DateTimes;
using OwnerSettlementsService.Data.Repositories;
using OwnerSettlementsService.Data.Repositories.Abstractions;

namespace OwnerSettlementsService.Api.Extensions
{
    public static class CustomDependenciesExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            //Datetimes
            services.AddScoped<IDateTimeBroker, SystemsDateTime>();
            //Payments
            services.AddScoped<IPaymentsService, PaymentsService>();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();
        }
    }
}
