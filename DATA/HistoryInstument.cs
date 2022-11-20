using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA
{
    public class HistoryInstument
    {
        [Key]
        public int InstrumentID { get;  set; }

        [ForeignKey("PortfolioID")]
        public HistoryPortfolio Portfolio { get;  set; }

        public string Symbol { get;  set; }
        public decimal MarketValue { get;  set; }
        public decimal Units { get;  set; }
        public decimal ProfitLose { get;  set; }
        public bool IsFromMainPortfolio { get;  set; }
    }
}
