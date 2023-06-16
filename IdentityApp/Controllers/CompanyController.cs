using IdentityApp.Models.ViewModels;
using IdentityEcommerce.Helpers.Enums;
using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Repositories;
using IdentityEcommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IdentityEcommerce.Controllers
{
    public class CompanyController : Controller
    {
        private readonly CompanyService _companiesService;
        private readonly UserManager<AppUser> _userManager;

        public CompanyController(CompanyService companiesService, UserManager<AppUser> userManager)
        {
            _companiesService = companiesService;
            _userManager = userManager;
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
        public IActionResult RegisterAdmin()
        {
            var adminAndCompanyViewModel = new AdminAndCompanyFormViewModel();
            return View(adminAndCompanyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(AdminAndCompanyFormViewModel adminAndCompanyFormViewModel)
        {
            //registering admin
            var role = AppRoleEnum.SuperUser.ToString();
            var newAdmin = adminAndCompanyFormViewModel.AdminForm;
            var adminRegister = await _userManager.CreateAsync(newAdmin);
            var assignRole = await _userManager.AddToRoleAsync(newAdmin, role);
            newAdmin.IsAdmin = true;

            //registering company
            var newCompany = adminAndCompanyFormViewModel.CompanyForm;
            bool createdCompany = _companiesService.Create(newCompany);
            if (createdCompany)
            {
                newAdmin.AssignedCompanyId = newCompany.CompanyID.ToString();
                await _userManager.UpdateAsync(newAdmin);
                return RedirectToAction("Index", "Product");
            }
            return View();
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
