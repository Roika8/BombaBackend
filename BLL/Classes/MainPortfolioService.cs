using BLL.Interfaces;
using DAL.Interfaces;
using DATA;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BLL
{
    public class MainPortfolioService : IMainPortfolioService
    {
        //private readonly IPortfolioRepository _portfolioRepository;

        private readonly IServiceScopeFactory _scopeFactory;

        public MainPortfolioService(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }
        //public MainPortfolioService(IPortfolioRepository portfolioRepository)
        //{
        //    this._portfolioRepository = portfolioRepository;
        //}
        public Task<bool> AddPortfolio(Portfolio portfolio)
        {
            var isSuccess = false;

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var portfolioRepo = scope.ServiceProvider.GetRequiredService<IPortfolioRepository>();
                portfolioRepo.AddPortfolioAsync(portfolio);
                isSuccess = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(isSuccess);
            }
            return Task.FromResult(isSuccess);

        }
    }
}
