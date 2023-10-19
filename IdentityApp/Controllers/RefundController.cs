using IdentityApp.Helpers.Enums;
using IdentityApp.Helpers.Points;
using IdentityApp.Migrations;
using IdentityApp.Models;
using IdentityApp.Models.ViewModels;
using IdentityApp.Services;
using IdentityEcommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IActionResult> Index()
        {
            List<Refund> refunds;
            if(GetCurrentUser().AssignedCompanyId  == null)
            {
                refunds = _refundService.GetAllRefundsRegularUser(GetCurrentUser().Id).ToList();
            }
            else
            {
                refunds = _refundService.GetAllRefunds(Int32.Parse(GetCurrentUser().AssignedCompanyId)).ToList();
            }
            foreach (var item in refunds)
            {
                item.CurrentUser = await _userManager.FindByIdAsync(item.UserId.ToString());
            }
            return View(refunds);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int productID, double transactionTotal, DateTime transactionDate, int transactionId)
        {
            bool validDeadline =_refundService.CheckRefundDeadline(transactionDate);
            if (!validDeadline)
            {
                return RedirectToAction("Activity", "Transaction");
            }
            var refund = new Refund();
            var user = GetCurrentUser();
            refund.UserId = user.Id;
            refund.ProductID = productID;
            refund.TransactionTotal = transactionTotal;
            refund.TransactionId = transactionId;
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
                return RedirectToAction("Index", "Refund");
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
        public async Task<IActionResult> ReviewRefund(int refundTotal, int refundId, int userId,int transactionId, bool accept)
        {
            if (accept)
            {
                var currentUser = await _userManager.FindByIdAsync(userId.ToString());
                await UpdateUserPoints(currentUser, PointConverter.ConvertMoneyToPoints(refundTotal));
                var refund = _refundService.GetRufundById(refundId);
                refund.Status = RefundStatus.Finished.ToString();
                var updated = _refundService.UpdateRefund(refund);
                // Get transaction -> Update transaction.RefundedStatus = true -> Save to Db
                var updatedTransaction = _refundService.UpdateTransactionAfterRefund(transactionId, true);
                return RedirectToAction("Index", "Refund");
            }
            else
            {
                var refund = _refundService.GetRufundById(refundId);
                refund.Status = RefundStatus.Rejected.ToString();
                var updated = _refundService.UpdateRefund(refund);
                // Get transaction -> Update transaction.RefundedStatus = true -> Save to Db
                var updatedTransaction = _refundService.UpdateTransactionAfterRefund(transactionId, true);
                return RedirectToAction("Index", "Refund");
            }
        }
    }
}
