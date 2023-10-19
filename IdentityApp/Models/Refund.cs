using IdentityEcommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApp.Models
{
    public class Refund
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("AspNetUsers")]
        public int UserId { get; set; }
        [NotMapped]
        public virtual AppUser CurrentUser { get; set; }

        [ForeignKey("Products")]
        public int ProductID { get; set; }
        [NotMapped]
        public virtual Product CurrentProducts { get; set; }
        [ForeignKey("Transaction")]
        public int TransactionId { get; set; }

        public double TransactionTotal { get; set; }
        public string SpecificReason { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

    }
}
