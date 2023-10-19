﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityEcommerce.Models
{
    public class Comment
    {
        // Properties
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(32)]
        public string Body { get; set; }

        // Foreign Keys
        [ForeignKey("AspNetUsers")]
        public int UserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("Products")]
        public int ProductID { get; set; }

        [ForeignKey("Reviews")]
        public int ReviewID { get; set; }

    }
}
