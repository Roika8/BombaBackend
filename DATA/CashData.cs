using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DATA
{
    public class CashData
    {
        [Key]
        public int CashDataID { get;  set; }

        [ForeignKey("UserID")]
        public virtual User User { get;  set; }

        public decimal Cash { get;  set; }
        public decimal Invested { get;  set; }

    }
}
