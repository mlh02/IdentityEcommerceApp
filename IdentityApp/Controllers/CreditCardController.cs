using IdentityApp.Models;
using IdentityApp.Services;
using IdentityEcommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityApp.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly CreditCardService _creditCardService;
        private readonly UserManager<AppUser> _userManager;

        public CreditCardController(CreditCardService creditCardService, UserManager<AppUser> userManager)
        {
            _creditCardService = creditCardService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var user = GetCurrentUser();
            var creditCard = new CreditCard();
            creditCard.UserID = user.Id;
            return View(creditCard);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreditCard creditCard)
        {
            var user = GetCurrentUser();
            bool createdCard = _creditCardService.Create(creditCard);
            if (createdCard)
            {
                user.HasCreditCard = true;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Settings", "AppUser");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Update()
        {
            var user = GetCurrentUser();
            var userCard = _creditCardService.GetCurrentUserCreditCard(user.Id);
            return View(userCard);
        }

        [HttpPost]
        public IActionResult Update(CreditCard creditCard)
        {
            bool updatedCard = _creditCardService.Update(creditCard);
            if (updatedCard)
            {
                return RedirectToAction("Settings", "AppUser");
            }
            return View();
        }

        public AppUser GetCurrentUser()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.FindByIdAsync(userID).Result;
            return user;
        }
    }
}
