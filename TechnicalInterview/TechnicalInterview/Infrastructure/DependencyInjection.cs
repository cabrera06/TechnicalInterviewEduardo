using TechnicalInterview.Core.Domain.Interfaces.Repositories;
using TechnicalInterview.Infrastructure.Repositories;

namespace TechnicalInterview.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            return services;
        }
    }
}
