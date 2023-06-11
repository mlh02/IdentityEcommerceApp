using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityEcommerce.Models
{
    public class Product
    {
        //Properties
        [Key]
        public int ProductID { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Stock { get; set; }
        public int RewardPoints { get; set; }

        //Foreign Keys
        [ForeignKey("Companies")] 
        public int CompanyID { get; set; }

        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }

        [ForeignKey("Categories")]
        public int CategoryID { get; set; }

    }
}
