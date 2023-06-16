using IdentityApp.Models;
using IdentityApp.Models.Repositories;

namespace IdentityApp.Services
{
    public class CouponService
    {
        private readonly CouponRepository _couponRepos;

        public CouponService(CouponRepository couponRepos)
        {
            _couponRepos = couponRepos;
        }

        public bool Create(Coupon coupon)
        {
            bool createdCoupon = _couponRepos.Create(coupon);
            return createdCoupon;
        }
    }
}
