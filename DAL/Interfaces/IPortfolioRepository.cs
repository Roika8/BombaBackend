using DATA.Instruments;
using DATA.Portfolios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<Portfolio> GetUserPorfolioAsync(Guid userID, int portfolioID);
        Task<bool> AddInstrumentToPortfolioAsync(PortfolioInstrument portfolio, Guid userID,int portfolioID);
        Task<int> CreatePortfolioAsync(Guid userID);
    }
}
