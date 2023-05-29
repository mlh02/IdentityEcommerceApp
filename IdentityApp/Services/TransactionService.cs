using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Repositories;
using System.Collections.Generic;
using System.Security.Claims;

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



    }
}
