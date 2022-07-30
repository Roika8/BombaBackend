using DAL.Interfaces;
using DATA;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly DataContext _dbContext;
        private readonly IServiceScopeFactory _scopeFactory;

        public PortfolioRepository(DataContext dbContext, IServiceScopeFactory scopeFactory)
        {
            this._dbContext = dbContext;
            _scopeFactory = scopeFactory;
        }
        private async Task<bool> AddPortfolio(Portfolio portfolio)
        {
            try
            {
                _dbContext.Portfolios.Add(portfolio);
                using var scope = _scopeFactory.CreateScope();
                var instrumentPortfolioRepo = scope.ServiceProvider.GetRequiredService<IPortfolioInstrumentRepository>();
                foreach (var instrument in portfolio.Instruments)
                {
                    await instrumentPortfolioRepo.AddInstrumentAsync(portfolio, instrument.Symbol, instrument.AvgPrice, instrument.Units);
                }
                return _dbContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public Task<bool> AddPortfolioAsync(Portfolio portfolio)
        {
            return Task.Run(() => AddPortfolio(portfolio));
        }
    }
}
