using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Instruments
{
    public abstract class BaseInstrument<T>
    {
        [Key]
        public Guid InstrumentID { get; set; }

        [ForeignKey("PortfolioID")]
        public virtual T Portfolio { get; set; }
        public string Symbol { get; set; }

    }
}
