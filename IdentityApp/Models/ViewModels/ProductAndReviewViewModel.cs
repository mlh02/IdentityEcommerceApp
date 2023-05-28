﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IdentityEcommerce.Models.ViewModels
{
    public class ProductAndReviewViewModel
    {
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
        public int CalculateAverageRating()
        {
            var averageRating = Reviews.Sum(x => x.Rating) / Reviews.Count();
            return averageRating;
        }
    }
}
