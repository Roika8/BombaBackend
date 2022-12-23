using System.ComponentModel.DataAnnotations.Schema;

namespace DATA
{
    public class TrackingInstumentPrice
    {
        [ForeignKey("InstrumentID")]
        public int InstrumentTrackingPriceID { get; set; }
        public decimal Price { get; set; }
    }
}
