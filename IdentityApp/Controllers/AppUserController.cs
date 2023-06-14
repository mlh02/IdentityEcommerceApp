using IdentityEcommerce.Helpers.Enums;
using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityEcommerce.Controllers
{
    public class AppUserController : Controller
    {
        private SignInManager<AppUser> _signInManager; // Manages your login, logout, getting users based on properties like name and Id
        private RoleManager<AppRole> _roleManager; // This manages roles, like assinging, creating new roles, or removing roles etc
        private UserManager<AppUser> _userManager; // Register users and validation

        public AppUserController(SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager, UserManager<AppUser> userManger)
        {
            _userManager = userManger;
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
            var userRegister = await _userManager.CreateAsync(appUser);
            // A regular user should have a role assigned on creation
            var assignRole = await _userManager.AddToRoleAsync(appUser, AppRoleEnum.User.ToString());
         
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
            AppUser user = await _userManager.FindByNameAsync(appLogin.Username);
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

        public IActionResult Settings()
        {
            var user = GetCurrentUser();
            return View(user);
        }

        [HttpGet]
        public IActionResult Update()
        {
            var user = GetCurrentUser();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUser user)
        {
            user.SecurityStamp = Guid.NewGuid().ToString();
            var currentUser = GetCurrentUser();
            var mappedUser = MapUserUpdates(user, currentUser);
            var updatedUser = await _userManager.UpdateAsync(mappedUser);
            return RedirectToAction("Settings", "AppUser");

        }

        public static AppUser MapUserUpdates(AppUser user, AppUser currentUser)
        {
            if (!String.IsNullOrEmpty(user.FirstName))
            {
                currentUser.FirstName = user.FirstName;
            }
            if (!String.IsNullOrEmpty(user.LastName))
            {
                currentUser.LastName = user.LastName;
            }
            if (!String.IsNullOrEmpty(user.Password))
            {
                currentUser.Password = user.Password;
            }
            if (!String.IsNullOrEmpty(user.ProfilePicture))
            {
                currentUser.ProfilePicture = user.ProfilePicture;
            }
            if (!String.IsNullOrEmpty(user.Email))
            {
                currentUser.Email = user.Email;
            }
            if (!String.IsNullOrEmpty(user.PhoneNumber))
            {
                currentUser.PhoneNumber = user.PhoneNumber;
            }
            if (!String.IsNullOrEmpty(user.UserName))
            {
                currentUser.UserName = user.UserName;
            }
            return currentUser;
        }

        public AppUser GetCurrentUser()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.FindByIdAsync(userID).Result;
            return user;
        }




    }
}
