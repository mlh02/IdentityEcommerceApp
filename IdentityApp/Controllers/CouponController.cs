using IdentityApp.Models;
using IdentityApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class CouponController : Controller
    {
        private readonly CouponService _couponService;

        public CouponController(CouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Coupon coupon)
        {
            bool createdCoupon = _couponService.Create(coupon);
            if (createdCoupon)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
    }
}
