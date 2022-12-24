using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DATA.Portfolios
{
    public abstract class BasePortfolio<T>
    {
        [Key]
        public Guid PortfolioID { get; set; }

        [ForeignKey("UserID")]
        public Guid UserID { get; set; }

        public virtual ICollection<T> Instruments { get; set; }

    }
}
