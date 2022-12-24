using DATA.Enums;
using DATA;
using System.ComponentModel.DataAnnotations.Schema;

namespace BombaRestAPI.Properties.DTOs
{
    public class PortfolioInstrumentDto
    {
        public string Symbol { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Units { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? TakeProfit { get; set; }
        public ChartPattern? ChartPattern { get; set; }
    }
}
