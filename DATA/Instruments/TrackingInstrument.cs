using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATA.Portfolios;

namespace DATA.Instruments
{
    public class TrackingInstrument : BaseInstrument<TrackingPortfolio>
    {
        public virtual ICollection<TrackingInstrumentPrice> TrackingPrices { get; set; }
    }
}
