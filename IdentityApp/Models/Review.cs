using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityEcommerce.Models
{
    public class Review
    {
        [Key]
        public int ID { get; set; }
            [ForeignKey("AspNetUsers")]
        public int UserId { get; set; }

        public virtual AppUser AppUser { get; set; }

        public string Body { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [ForeignKey("Products")]
        public int ProductID { get; set; }
        public int Rating { get; set; }
    }

}
