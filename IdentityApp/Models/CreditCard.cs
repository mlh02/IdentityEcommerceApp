using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityApp.Models
{
    public class CreditCard
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Expiry { get; set; }
        public int CVV { get; set; }

        [ForeignKey("AspNetUsers")]
        public int UserID { get; set; }

    }
}
