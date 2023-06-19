using IdentityApp.Helpers.Points;
using IdentityApp.Models;
using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace IdentityEcommerce.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepos;

        public TransactionService(TransactionRepository transactionRepos)
        {
            _transactionRepos = transactionRepos;
        }

        public Product FindProductByIdForTransaction(int Id)
        {
            var ProductFound = _transactionRepos.FindProductByIdForTransaction(Id);
            return ProductFound;
        }

        public bool Create(Transaction transaction)
        {

            bool createdTransaction = _transactionRepos.Create(transaction);
            return createdTransaction;
        }
       
        public IEnumerable<Transaction> GetAllTransactions()
        {
            var allTransactions = _transactionRepos.GetAllTransactions();
            return allTransactions;
        }

        public double CalculateTransactionTotal(Transaction transaction)
        {
            transaction.Total = transaction.CurrentProduct.Price * transaction.QuantityBought;
            return transaction.Total;

        }

        public bool ValidatePointsForTransaction(int userPoints, double transactionTotal)
        {
            var userDollars = PointConverter.ConvertPoints(userPoints);
            if (userDollars < transactionTotal)
            {
                return false;
            }
            return true;
        }

        public bool ValidateUserCoupon(Transaction transaction)
        {
            var coupon = _transactionRepos.GetAllCoupons().SingleOrDefault(x => x.Code.Equals(transaction.CouponCode));
            if (coupon == null) return false;
            if (coupon.Quantity == 0)
            {
                coupon.Active = false;
                return false;
            }
            transaction.Total -= transaction.Total * (coupon.Percentage / 100);
            coupon.Quantity--;
            return true;
        }



    }
}
