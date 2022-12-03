using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATA.Portfolios;

namespace DATA.Instruments
{
    public class HistoryInstrument : BaseInstrument<HistoryPortfolio>
    {
        public decimal MarketValue { get; set; }
        public decimal Units { get; set; }
        public decimal ProfitLose { get; set; }
        public bool IsFromMainPortfolio { get; set; }
    }
}
