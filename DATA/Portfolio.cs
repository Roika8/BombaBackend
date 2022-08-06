using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA
{
    public class Portfolio
    {
        [Key]
        public int PortfolioID { get; set; }

        [ForeignKey("UserID")]
        public Guid UserID { get; set; }

        public ICollection<PortfolioInstrument> Instruments { get; set; }
    }
}

