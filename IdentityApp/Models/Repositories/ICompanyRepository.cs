using System.Collections.Generic;

namespace IdentityEcommerce.Models.Repositories
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies();
        bool Delete(int companyID);
        bool Create(Company company);
        

    }
}
