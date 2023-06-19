using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityApp.Models
{
    public class Coupon
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public double Percentage { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int Quantity { get; set; }
        public bool Active { get; set; }
    }
}
