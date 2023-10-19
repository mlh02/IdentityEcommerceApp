using IdentityEcommerce.Helpers.User;
using IdentityEcommerce.Models;
using IdentityEcommerce.Services;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Create(Transaction transaction)
        {
            double transactionTotal = _transactionService.CalculateTransactionTotal(transaction);
            bool isCouponValid = _transactionService.ValidateUserCoupon(transaction);
            var user = GetCurrentUser();
            transaction.UserId = user.Id.ToString();
            if (transaction.PayPoints)
            {
                bool validPoints = await ValidatePointsForTransaction(transaction, transactionTotal, user);
                if (!validPoints)
                {
                    return View(transaction);
                }
            }
            else
            {
                if (!user.HasCreditCard)
                {
                    return RedirectToAction("Create", "CreditCard");
                }
                await UpdateUserRewardPoints(transaction.CurrentProduct.RewardPoints);
            }

            bool createdTransaction = _transactionService.Create(transaction);
            if (createdTransaction)
            {
                return RedirectToAction("Index", "Product");
            }
            return View(transaction);
        }

        public async Task<bool> ValidatePointsForTransaction(Transaction transaction, double transactionTotal, AppUser user)
        {
            var validatedPoints = _transactionService.ValidatePointsForTransaction(user.MyRewardPoints, transactionTotal);
            if (validatedPoints)
            {
                await UpdateUserRewardPoints(transaction.CurrentProduct.RewardPoints, true, transactionTotal);
                return true;
            }
            return false;
        }

        public async Task UpdateUserRewardPoints(int rewardPoints, bool payPoints = false, double transactionTotal = 0)
        {
            var user = GetCurrentUser();
            if (payPoints)
            {
                user.MyRewardPoints -= Convert.ToInt32(transactionTotal / 0.6);
            }
            user.MyRewardPoints += rewardPoints;
            await _userManager.UpdateAsync(user);
        }

        public AppUser GetCurrentUser()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.FindByIdAsync(userID).Result;
            return user;
        }

        public IActionResult Activity()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            var transactions = _transactionService.GetAllTransactions();
            var userTransactions = transactions.Where(x => x.UserId == userID);
            return View(userTransactions);
        }

    }
}
