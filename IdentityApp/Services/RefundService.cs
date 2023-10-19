using IdentityApp.Helpers.Enums;
using IdentityApp.Models;
using IdentityApp.Models.Repositories;
using IdentityEcommerce.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApp.Services
{
    public class RefundService
    {
        private readonly RefundRepository _refundRepos;

        public RefundService(RefundRepository refundRepos)
        {
            _refundRepos = refundRepos;
        }

        public bool Create(Refund refund)
        {
            refund.Status = RefundStatus.Active.ToString();
            if(refund.Notes == null)
            {
                refund.Notes = "NR";
            }
            var createdRefund = _refundRepos.Create(refund);
            return createdRefund;
        }

        public IEnumerable<Product> GetAllRefundProducts()
        {
            var products = _refundRepos.GetAllRefundProducts();
            return products;
        }
        public IEnumerable<Refund> GetAllRefunds(int companyId)
        {
            var refunds = _refundRepos.GetAllRefunds(companyId);
            return refunds;
        }
        public IEnumerable<Refund> GetAllRefundsRegularUser(int userId)
        {
            var refunds = _refundRepos.GetAllRefundsRegularUser(userId);
            return refunds;
        }
         
        public Refund GetRufundById(int refundId)
        {
            return _refundRepos.GetRefundById(refundId);
        }

        public bool CheckRefundDeadline(DateTime transactionDate)
        {
            var daysElapsed = (DateTime.Now - transactionDate).TotalDays;
            var validDeadline = daysElapsed < 30;
            return validDeadline;
        }
        public bool UpdateRefund(Refund refund)
        {
            if (_refundRepos.UpdateRefund(refund))
            {
                return true;
            }
            return false;
        }
        public bool UpdateTransactionAfterRefund(int transactionId, bool boolean)
        {
            return _refundRepos.UpdateTransactionAfterRefund(transactionId, boolean);
        }
        public int CalculateReturnedPoints(int userPoints, double transactionTotal)
        {
            var pointsReturned = Convert.ToInt32(transactionTotal / 0.6);
            return pointsReturned;

        }
    }
}
