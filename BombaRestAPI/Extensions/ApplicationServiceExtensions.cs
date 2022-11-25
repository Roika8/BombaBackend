using BLL.Classes;
using BLL.Core;
using BLL.Interfaces;
using BLL;
using DAL.Interfaces;
using DAL.Repositories;
using DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using BLL.PortfolioInstruments;

namespace BombaRestAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IPortfolioInstrumentRepository, PortfolioInstrumentRepository>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddSingleton<IMainPortfolioService, MainPortfolioService>();
            services.AddSingleton<IUserService, UserService>();

            services.AddDbContext<MainDataContext>(ops =>
            {
                ops.UseLazyLoadingProxies().UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<UserDataContext>(ops =>
            {
                ops.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BombaAPI", Version = "v1" });
            });

            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(Mapping).Assembly);


            return services;
        }

    }
}
