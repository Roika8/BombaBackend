using DAL.Interfaces;
using DATA;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private int CreatePortfolio(Guid userID)
        {
            try
            {
                _dbContext.Portfolios.Add(new Portfolio
                {
                    UserID = userID
                });
                _dbContext.SaveChanges();
                return _dbContext.Portfolios.FirstOrDefault(p => p.UserID == userID).PortfolioID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        private Portfolio GetUserPorfolio(Guid userID, int portfolioID)
        {
            var p = _dbContext.Portfolios.FirstOrDefault(p => p.UserID == userID && p.PortfolioID == portfolioID);
            return p;
        }
        public Task<bool> AddInstrumentToPortfolioAsync(PortfolioInstrument portfolioInstrument, Guid userID, int portfolioID)
        {
            return Task.Run(() => AddInstrumentToPortfolio(portfolioInstrument, userID, portfolioID));

        }

        private bool AddInstrumentToPortfolio(PortfolioInstrument portfolioInstrument, Guid userID, int portfolioID)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var instrumentPortfolioRepo = scope.ServiceProvider.GetRequiredService<IPortfolioInstrumentRepository>();
                var portfolioRepo = scope.ServiceProvider.GetRequiredService<IPortfolioRepository>();
                var portfolio = _dbContext.Portfolios.FirstOrDefault(p => p.UserID == userID && p.PortfolioID == portfolioID);
                portfolio.Instruments??=new List<PortfolioInstrument>();
                portfolio.Instruments.Add(portfolioInstrument);
                _dbContext.Portfolios.Update(portfolio);
                return _dbContext.SaveChanges() > 0;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Task<int> CreatePortfolioAsync(Guid userID)
        {
            return Task.Run(() => CreatePortfolio(userID));
        }



        public Task<Portfolio> GetUserPorfolioAsync(Guid userID, int portfolioID)
        {
            return Task.Run(() => GetUserPorfolio(userID, portfolioID));
        }
    }


}
