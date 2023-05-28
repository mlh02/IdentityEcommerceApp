using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityEcommerce.Models
{
    public class Review
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("AspNetUsers")]
        public virtual int UserID { get; set; }
        public string Body { get; set; }

        [ForeignKey("Products")]
        public int ProductID { get; set; }
        public int Rating { get; set; }
    }
}
