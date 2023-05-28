using IdentityEcommerce.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace IdentityEcommerce.Models.Repositories
{
    public interface ITransactionRepository 
    {
        IEnumerable<Transaction> GetAllTransactions();
        bool Create(Transaction transaction);
        Product FindProductByIdForTransaction(int Productid);
    }
}
