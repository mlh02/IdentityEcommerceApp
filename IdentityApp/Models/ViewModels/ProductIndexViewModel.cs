using IdentityEcommerce.Models;
using System.Collections.Generic;
using System.Linq;

namespace IdentityApp.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public AppUser CurrentUser { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Company> Companies { get; set; }
        public List<Review> Reviews { get; set; }
        public int CalculateAverageRating(List<Review> reviews)
        {
            if (reviews.Count > 0)
            {
                var averageRating = reviews.Sum(x => x.Rating) / reviews.Count();
                return averageRating;
            }
            return 0;
        }

    }
}
