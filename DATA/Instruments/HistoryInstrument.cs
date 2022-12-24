using DATA.Portfolios;
using System;

namespace DATA.Instruments
{
    public class HistoryInstrument : BaseInstrument<HistoryPortfolio>
    {
        public decimal ActionOccuredPrice { get; set; }
        public decimal Units { get; set; }
        public decimal ProfitLoss { get; set; }
        public DateTime RequestOccured { get; set; }
    }
}
