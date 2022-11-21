using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA
{
    public class TrackingPortfolio
    {
        [Key]
        public int PortfolioID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public virtual ICollection<TrackingInstrument> Instruments { get; set; }
    }
}
