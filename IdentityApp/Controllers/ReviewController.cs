using IdentityEcommerce.Helpers.User;
using IdentityEcommerce.Models;
using IdentityEcommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityEcommerce.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewService _reviewService;
        private readonly UserManager<AppUser> _userManager;

        public ReviewController(ReviewService reviewService, UserManager<AppUser> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create(int productID)
        {
            var user = GetCurrentUser();
            Review review = new Review();
            review.ProductID = productID;
            review.UserID = user.Id;
            return View(review);
        }


        [HttpPost]
        public IActionResult Create(Review review)
        {
            bool createdReview = _reviewService.Create(review);
            if (createdReview)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }


        public AppUser GetCurrentUser()
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.FindByIdAsync(userID).Result;
            return user;
        }

    }
}
