using IdentityApp.Models;
using IdentityApp.Models.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace IdentityApp.Services
{
    public class CreditCardService
    {
        private readonly CreditCardRepository _creditCardRepos;

        public CreditCardService(CreditCardRepository creditCardRepos)
        {
            _creditCardRepos = creditCardRepos;
        }

        public bool Create(CreditCard creditCard)
        {
            bool createdCard = _creditCardRepos.Create(creditCard);
            return createdCard;
        }

        public bool Update(CreditCard creditCard)
        {
            bool updatedCard = _creditCardRepos.Update(creditCard);
            return updatedCard;
        }

        public CreditCard GetCurrentUserCreditCard(int userID)
        {
            var userCard = _creditCardRepos.GetAllCreditCards().SingleOrDefault(x => x.UserID == userID);
            return userCard;
        }


    }
}
