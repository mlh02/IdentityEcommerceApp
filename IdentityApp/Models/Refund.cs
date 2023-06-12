using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApp.Models
{
    public class Refund
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("AspNetUsers")]
        public int UserID { get; set; }

        [ForeignKey("Products")]
        public int ProductID { get; set; }
        
        public double TransactionTotal { get; set; }
        public string SpecificReason { get; set; }
        public string Notes { get; set; }

    }
}
