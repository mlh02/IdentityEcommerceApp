using IdentityApp.Services;
using IdentityEcommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(CommentService commentService, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create(int productID, int reviewID)
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            var comment = new Comment();
            comment.ProductID = productID;
            comment.ReviewID = reviewID;
            comment.UserId = int.Parse(userID);
            return View(comment);
        }

        [HttpPost]
        public IActionResult Create(Comment comment)
        {
            bool createdComment = _commentService.Create(comment);
            if (createdComment)
            {
                return RedirectToAction("Details", "Product", new {productID = comment.ProductID});
            }
            return View();
        }

    }
}
