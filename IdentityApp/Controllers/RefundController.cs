using IdentityApp.Models;
using IdentityApp.Services;
using IdentityEcommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityApp.Controllers
{
    public class RefundController : Controller
    {
        private readonly RefundService _refundService;
        private readonly UserManager<AppUser> _userManager;

        public RefundController(RefundService refundService, UserManager<AppUser> userManager)
        {
            _refundService = refundService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var refunds = _refundService.GetAllRefunds();
            return View(refunds);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int productID, double transactionTotal, DateTime transactionDate)
        {
            bool validDeadline =_refundService.CheckRefundDeadline(transactionDate);
            if (!validDeadline)
            {
                return RedirectToAction("Activity", "Transaction");
            }
            var refund = new Refund();
            var user = GetCurrentUser();
            refund.UserID = user.Id;
            refund.ProductID = productID;
            refund.TransactionTotal = transactionTotal;
            var returnedPoints = _refundService.CalculateReturnedPoints(user.MyRewardPoints, transactionTotal);
            await UpdateUserPoints(user, returnedPoints);
            return View(refund);
        }

        [HttpPost]
        public IActionResult Create(Refund refund)
        {
            bool createdRefund = _refundService.Create(refund);
            if (createdRefund)
            {
                return RedirectToAction("Activity", "Transaction");
            }
            return View();
        }
        public AppUser GetCurrentUser()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.FindByIdAsync(userID).Result;
            return user;
        }
        
        public async Task UpdateUserPoints(AppUser user, int returnedPoints)
        {
            user.MyRewardPoints += returnedPoints;
            await _userManager.UpdateAsync(user);
        }
    }
}
