using IdentityApp.Models;
using IdentityEcommerce.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace IdentityEcommerce.Models.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Transaction transaction)
        {

            transaction.CurrentProduct = null;
            try
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
       
        public Product FindProductByIdForTransaction(int Id)
        {
             var currProductForBuying =_context.Products.FirstOrDefault(x => x.ProductID == Id);
             return currProductForBuying; 
        }


        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }

        public IEnumerable<Coupon> GetAllCoupons()
        {
            var coupons = _context.Coupons.ToList();
            return coupons;
        }
    }
}
