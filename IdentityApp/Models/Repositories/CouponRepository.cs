using IdentityEcommerce.Data;

namespace IdentityApp.Models.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ApplicationDbContext _context;

        public CouponRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Coupon coupon)
        {
            try
            {
                _context.Coupons.Add(coupon);
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
