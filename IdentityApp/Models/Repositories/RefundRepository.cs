using IdentityApp.Migrations;
using IdentityEcommerce.Data;
using IdentityEcommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IdentityApp.Models.Repositories
{
    public class RefundRepository : IRefundRepository
    {
        private readonly ApplicationDbContext _context;

        public RefundRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Refund refund)
        {
            try
            {
                _context.Refunds.Add(refund);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public bool UpdateTransactionAfterRefund(int transactionId, bool boolean)
        {
            try
            {
                var transaction = _context.Transactions.FirstOrDefault(x => x.TransactionID == transactionId);
                transaction.RefundedStatus = boolean;
                _context.Transactions.Update(transaction);
                _context.SaveChanges();
                return true;

            }
            catch (System.Exception)
            {
                return false;
            }

        }
        public bool UpdateRefund(Refund refund)
        {
            try
            {
                _context.Refunds.Update(refund);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false ;
            }
        }
        public  Refund  GetRefundById(int refundId)
        {
            var refunds = _context.Refunds.FirstOrDefault(x => x.ID == refundId);
            refunds.CurrentProducts = _context.Products.FirstOrDefault(x => x.ProductID == refunds.ProductID);
            return refunds;
        }
        public IEnumerable<Refund> GetAllRefundsRegularUser(int userId)
        {
            var refunds = _context.Refunds.Where(x => x.UserId == userId).ToList();
            foreach (var item in refunds)
            {
                item.CurrentProducts = _context.Products.FirstOrDefault(x => x.ProductID == item.ProductID);
            }
            return refunds;
        }
        public IEnumerable<Refund> GetAllRefunds(int companyId)
        {
            var refunds = _context.Refunds.Where(x => x.Status == "Active").ToList();
            foreach (var item in refunds)
            {
                item.CurrentProducts = _context.Products.FirstOrDefault(x => x.ProductID == item.ProductID);
            }
            return refunds.Where(x => x.CurrentProducts.CompanyID == companyId);
        }
        public IEnumerable<Product> GetAllRefundProducts()
        {
            var products = _context.Products
                        .ToList();
            return products;
        }
    }
}
