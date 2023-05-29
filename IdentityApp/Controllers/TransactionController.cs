using IdentityEcommerce.Models;
using IdentityEcommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityEcommerce.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionService _transactionService;
        private readonly UserManager<AppUser> _userManager;

        public TransactionController(TransactionService transactionService, UserManager<AppUser> userManager)
        {
            _transactionService = transactionService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var allTransactions = _transactionService.GetAllTransactions();
            return View(allTransactions);
        }

        [HttpGet]
        public IActionResult Create(int Productid)
        {
            var ProductFound = _transactionService.FindProductByIdForTransaction(Productid);
            var CurrTransaction = new Transaction();
            CurrTransaction.CurrentProduct = ProductFound;
            return View(CurrTransaction);
        }

        [HttpPost]
        public async Task<IActionResult> BuyAction(Transaction transaction)
        {
            var user = GetCurrentUser();
            bool createdTransaction = _transactionService.Create(transaction, user);
            if (createdTransaction)
            {
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        public AppUser GetCurrentUser()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.FindByIdAsync(userID).Result;
            return user;
        }
    }
}
