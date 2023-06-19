using IdentityEcommerce.Models;
using System.Collections.Generic;

namespace IdentityApp.Models.ViewModels
{
    public class AdminAndCompanyFormViewModel
    {
        public AppUser AdminForm { get; set; }
        public Company CompanyForm { get; set; }
        public List<Company> Companies { get; set; }
    }
}
