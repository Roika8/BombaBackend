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
        public virtual TrackingPortfolio Portfolio { get; set; }

        public string Symbol { get; set; }
        public virtual ICollection<TrackingInstumentPrice> TrackingPrices { get; set; }
    }
}
