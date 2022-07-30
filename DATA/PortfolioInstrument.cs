using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA
{
    public class PortfolioInstrument
    {
        [Key]
        public int InstrumentID { get; set; }

        [ForeignKey("PortfolioID")]
        public Portfolio Portfolio { get; set; }

        public string Symbol { get; set; }
        public decimal AvgPrice { get; set; }
        public decimal Units { get; set; }
    }
}
