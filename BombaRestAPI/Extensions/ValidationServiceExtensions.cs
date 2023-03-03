using BLL.MainPortfolio.Validators;
using DATA.Instruments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BombaRestAPI.Extensions
{
    public static class ValidationServiceExtensions
    {
        public static IServiceCollection AddValidationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<ICommandValidator<PortfolioInstrument>, AddPortfolioInstrumentValidator>();
            //todo add more validators init
            return services;
        }
    }
}
