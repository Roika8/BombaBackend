
using System;
using System.ComponentModel.DataAnnotations;

namespace DATA.Instruments
{
    public abstract class BaseInstrument<T>
    {
        [Key]
        public int InstrumentId { get; set; }
        public string Symbol { get; set; }

        public Guid PortfolioId { get; set; }
        public virtual T Portfolio { get; set; }

    }
}
