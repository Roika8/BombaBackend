using DATA;
using System.Collections.Generic;

namespace BombaRestAPI.Properties.DTOs.InstumentsDtos
{
    public class TrackingInstrumentDto
    {
        public string Symbol { get; set; }

        public List<decimal> TrackingPrices { get; set; }

    }
}
