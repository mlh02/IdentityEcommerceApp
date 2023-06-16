using System.Collections.Generic;

namespace IdentityApp.Models.Repositories
{
    public interface ICreditCardRepository
    {
        bool Create(CreditCard creditCard);
        bool Update(CreditCard creditCard);
        IEnumerable<CreditCard> GetAllCreditCards();
    }
}
