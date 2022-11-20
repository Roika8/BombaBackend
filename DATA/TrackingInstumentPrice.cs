using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA
{
    public class TrackingInstumentPrice
    {
        [Key]
        public int InstrumentTrackingPriceID { get; set; }
        public decimal Price { get; set; }
    }
}
