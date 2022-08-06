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

        public async Task<bool> AddInstrumentToPortfolio(PortfolioInstrument portfolioInstrument, Guid userID)
        {
            try
            {
                bool isSuccess = false;
                int portfolioID = portfolioInstrument.Portfolio.PortfolioID;
                using var scope = _scopeFactory.CreateScope();
                var portfolioRepo = scope.ServiceProvider.GetRequiredService<IPortfolioRepository>();
                Portfolio portfolio = await portfolioRepo.GetUserPorfolioAsync(userID, portfolioID);
                if (portfolio == null)
                {
                    throw new ArgumentOutOfRangeException($"Cannot find portfolio for userID:  {userID} and portfolioID {portfolioID}");
                }

                return await portfolioRepo.AddInstrumentToPortfolioAsync(portfolioInstrument, userID, portfolioID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
