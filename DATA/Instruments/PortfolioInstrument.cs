using DATA.Enums;
using DATA.Portfolios;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Instruments
{
    public class PortfolioInstrument : BaseInstrument<Portfolio>
    {
        public decimal AvgPrice { get; set; }
        public decimal Units { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }
        public ChartPattern ChartPattern { get; set; }

    }
}
