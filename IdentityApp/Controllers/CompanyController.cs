using IdentityEcommerce.Helpers.Enums;
using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Repositories;
using IdentityEcommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityEcommerce.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyService _companiesService;

        public CompanyController(CompanyService companiesService)
        {
            _companiesService = companiesService;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(AppRoleEnum.SuperUser.ToString())) 
            {
                var allCompanies = _companiesService.GetCompanies();
                return View(allCompanies);
            }
            return RedirectToAction("Login", "AppUser");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            bool createdCompany = _companiesService.Create(company);
            if (createdCompany)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int companyID)
        {
            _companiesService.Delete(companyID);
            return RedirectToAction("Index");
        }
    }

}
