﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace IdentityEcommerce.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }

        public double Total { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public int QuantityBought { get; set; }
        public virtual Product CurrentProduct { get; set; }
        public bool PayPoints { get; set; }
        public bool RefundedStatus { get; set; } = false;

        //Foreign keys
        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        [ForeignKey("Products")]
        public int ProductID { get; set; }
        [ForeignKey("Coupons")]
        public string CouponCode { get; set; }
    }
}
