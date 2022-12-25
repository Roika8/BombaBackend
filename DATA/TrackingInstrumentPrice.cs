using DATA.Instruments;
using System;

namespace DATA
{
    public class TrackingInstrumentPrice
    {
        public int ID { get; set; }
        public int InstrumentID { get; set; }
        public decimal Price { get; set; }
        public virtual TrackingInstrument Instrument { get; set; }
    }
}
