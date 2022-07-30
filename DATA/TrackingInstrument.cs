using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DATA
{
    public class TrackingInstrument
    {
        [Key]
        public int InstrumentID { get; set; }

        [ForeignKey("PortfolioID")]
        public TrackingPortfolio Portfolio { get; set; }

        public string Symbol { get; set; }
        public ICollection<TrackingInstumentPrice> TrackingPrices { get; set; }
    }
}
