
using DATA;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPortfolioInstrumentRepository
    {
        Task<bool> AddInstrumentAsync(Portfolio portfolio, string symbol, decimal avgPrice, decimal units);
        Task<List<PortfolioInstrument>> GetAllPorfolioInstumentsAsync(int portfolioID);
        Task<bool> RemoveInstrumentAsync(int instrumentID);
        Task<bool> EditInstrumentAsync(int instrumentID, decimal units, decimal avgPrice);
    }
}
