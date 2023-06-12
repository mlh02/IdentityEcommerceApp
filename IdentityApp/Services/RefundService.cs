using IdentityApp.Models;
using IdentityApp.Models.Repositories;
using IdentityEcommerce.Models;
using System;
using System.Collections;
using System.Collections.Generic;
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
            var createdRefund = _refundRepos.Create(refund);
            return createdRefund;
        }

        public IEnumerable<Refund> GetAllRefunds()
        {
            var refunds = _refundRepos.GetAllRefunds();
            return refunds;
        }

        public bool CheckRefundDeadline(DateTime transactionDate)
        {
            var daysElapsed = (DateTime.Now - transactionDate).TotalDays;
            var validDeadline = daysElapsed < 30;
            return validDeadline;
        }

        public int CalculateReturnedPoints(int userPoints, double transactionTotal)
        {
            var pointsReturned = Convert.ToInt32(transactionTotal / 0.6);
            return pointsReturned;

        }
    }
}
