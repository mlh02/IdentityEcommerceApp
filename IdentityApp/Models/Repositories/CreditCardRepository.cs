using IdentityEcommerce.Data;
using System.Collections.Generic;
using System.Linq;

namespace IdentityApp.Models.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly ApplicationDbContext _context;

        public CreditCardRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(CreditCard creditCard)
        {
            try
            {
                _context.CreditCards.Add(creditCard);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public IEnumerable<CreditCard> GetAllCreditCards()
        {
            var creditCards = _context.CreditCards.ToList();
            return creditCards;
        }

        public bool Update(CreditCard creditCard)
        {
            try
            {
                _context.CreditCards.Update(creditCard);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }
    }
}
