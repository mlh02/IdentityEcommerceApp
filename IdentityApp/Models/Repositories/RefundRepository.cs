using IdentityEcommerce.Data;
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

        public IEnumerable<Refund> GetAllRefunds()
        {
            var refunds = _context.Refunds.ToList();
            return refunds;
        }
    }
}
