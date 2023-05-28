using System.ComponentModel.DataAnnotations;

namespace IdentityEcommerce.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public int TotalSales { get; set; }
    }
}
