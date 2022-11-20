using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DATA
{
    public class HistoryPortfolio
    {
        [Key]
        public int PortfolioID { get;  set; }

        [ForeignKey("UserID")]
        public User User { get;  set; }

        public ICollection<HistoryInstument> Instruments { get;  set; }
    }
}