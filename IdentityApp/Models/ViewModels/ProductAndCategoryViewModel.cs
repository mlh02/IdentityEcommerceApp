using IdentityEcommerce.Models;
using System.Collections.Generic;

namespace IdentityApp.Models.ViewModels
{
    public class ProductAndCategoryViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
