using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Repositories;
using System.Collections.Generic;

namespace IdentityEcommerce.Services
{
    public class CompanyService
    {
        private readonly CompanyRepository _companiesRepo;

        public CompanyService(CompanyRepository companiesRepo)
        {
            _companiesRepo = companiesRepo;
        }

        public IEnumerable<Company> GetCompanies()
        {
            var allCompanies = _companiesRepo.GetAllCompanies();
            return allCompanies;
        }

        public bool Create(Company company)
        {
            bool createdCompany = _companiesRepo.Create(company);
            return createdCompany;
        }

        public void Delete(int companyID)
        {
            _companiesRepo.Delete(companyID);            
        }

    }
}
