using DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMainPortfolioService
    {
        Task<bool> AddInstrumentToPortfolio(PortfolioInstrument portfolioInstrument,Guid userID);
        //    public Portfolio GetPortfolio(Guid userID);
        //    public void AddIntumentToPortfolio(string portfolioID, string symbol, decimal avgPrice, decimal units);
        //     SetSymbolUnits(string portfolioID, string symbol, decimal units);
    }
}
