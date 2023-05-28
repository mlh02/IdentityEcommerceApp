using IdentityEcommerce.Helpers.Enums;
using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityEcommerce.Controllers
{
    public class AppUserController : Controller
    {
        private SignInManager<AppUser> _signInManager; // Manages your login, logout, getting users based on properties like name and Id
        private RoleManager<AppRole> _roleManager; // This manages roles, like assinging, creating new roles, or removing roles etc
        private UserManager<AppUser> _userManger; // Register users and validation

        public AppUserController(SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, UserManager<AppUser> userManger)
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _roleManager = roleManager;
  
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AppUser appUser)
        {

            var role = AppRoleEnum.User.ToString();
            // this is when we send the user over and have it validated and stored in DB
            var userRegister = await _userManger.CreateAsync(appUser);
            // A regular user should have a role assigned on creation
            var assignRole = await _userManger.AddToRoleAsync(appUser, AppRoleEnum.User.ToString());
         
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AppLogin appLogin)
        {
            AppUser user = await _userManger.FindByNameAsync(appLogin.Username);
            if(user.Password == appLogin.Password)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Product");

            }
            return View();
        }
        public async Task<RedirectToActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Register", "AppUser");
        }


    }
}
