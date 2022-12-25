using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA
{
    public class TrackingInstumentPrice
    {
        [ForeignKey("InstrumentID")]
        public Guid InstrumentID { get; set; }
        public decimal Price { get; set; }
    }
}
