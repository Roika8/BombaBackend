using DAL.Interfaces;
using DATA.Instruments;
using DATA.Portfolios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PortfolioInstrumentRepository : IPortfolioInstrumentRepository
    {
        private MainDataContext dbContext;
        public PortfolioInstrumentRepository(MainDataContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        private bool AddInstrument(PortfolioInstrument instrument)
        {
            dbContext.PortfolioInstruments.Add(instrument);
            return dbContext.SaveChanges() > 0;
        }
        public Task<bool> AddInstrumentAsync(Portfolio portfolio, string symbol, decimal avgPrice, decimal units)
        {
            PortfolioInstrument instrument = new()
            {
                AvgPrice = avgPrice,
                Symbol = symbol,
                Units = units,
                Portfolio = portfolio,
            };
            return Task.Run(() => AddInstrument(instrument));
        }

        private bool EditInstrument(int instrumentID, decimal units, decimal avgPrice)
        {
            PortfolioInstrument foundInstrument = dbContext.PortfolioInstruments.FirstOrDefault(p => p.InstrumentID == instrumentID);
            foundInstrument.Units = units;
            foundInstrument.AvgPrice = avgPrice;
            return dbContext.SaveChanges() > 0;
        }
        public Task<bool> EditInstrumentAsync(int instrumentID, decimal units, decimal avgPrice)
        {
            return Task.Run(() => EditInstrument(instrumentID, units, avgPrice));
        }

        private bool RemoveInstrument(int instrumentID)
        {
            PortfolioInstrument foundInstrument = dbContext.PortfolioInstruments.FirstOrDefault(p => p.InstrumentID == instrumentID);
            dbContext.PortfolioInstruments.Remove(foundInstrument);
            return dbContext.SaveChanges() > 0;
        }

        public Task<bool> RemoveInstrumentAsync(int instrumentID)
        {
            return Task.Run(() => RemoveInstrument(instrumentID));
        }

        private List<PortfolioInstrument> GetAllPorfolioInstruments(int portfolioID)
        {
            List<PortfolioInstrument> portfolioInstruments = dbContext.PortfolioInstruments.Where
                                                             (inst => inst.Portfolio.PortfolioID == portfolioID).ToList();
            return portfolioInstruments;
        }
        public Task<List<PortfolioInstrument>> GetAllPorfolioInstumentsAsync(int portfolioID)
        {
            return Task.Run(() => GetAllPorfolioInstruments(portfolioID));
        }
    }
}
