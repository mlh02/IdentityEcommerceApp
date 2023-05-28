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

            transaction.Total = transaction.CurrentProduct.Price * transaction.QuantityBought;
            transaction.UserId = transaction.CurrentProduct.UserId;
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
    }
}
