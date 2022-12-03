using DATA;
using System;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGenericPortfolioService
    {
        Task<bool> AddInstrumentToPortfolio<T>(T instrument);
        Task<bool> EditPortfolioInstrument<T>(T instrument);
        Task<bool> DeletePortfolioInstrument<T>(T instrument);
        Task<T> GetPortfolioInstrument<T>(int instrumentId);

        //    public Portfolio GetPortfolio(Guid userID);
        //    public void AddIntumentToPortfolio(string portfolioID, string symbol, decimal avgPrice, decimal units);
        //     SetSymbolUnits(string portfolioID, string symbol, decimal units);
    }
}
