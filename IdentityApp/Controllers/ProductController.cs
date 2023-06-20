using IdentityApp.Migrations;
using IdentityApp.Models.Repositories;
using IdentityApp.Models.ViewModels;
using IdentityEcommerce.Helpers.Enums;
using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Repositories;
using IdentityEcommerce.Models.ViewModels;
using IdentityEcommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace IdentityEcommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(ProductService productService, UserManager<AppUser> userManager)
        {
            _productService = productService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(AppRoleEnum.User.ToString()) || User.IsInRole(AppRoleEnum.SuperUser.ToString()))
            {
                var allProducts = _productService.GetAllProducts();
                return View(allProducts);
            }
            return RedirectToAction("Login", "AppUser");

        }

        [HttpGet]
        public IActionResult Create()
        {
            var pcvm = new ProductAndCategoryViewModel();
            pcvm.Categories = _productService.GetAllCategories();
            return View(pcvm);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool createdProduct = _productService.Create(product);
            if (createdProduct)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Details(int productID)
        {
            var prvm = new ProductAndReviewViewModel();
            prvm.Product = _productService.GetProductByID(productID);
            prvm.Reviews = _productService.GetReviewsOfSpecificProduct(productID);
            prvm.CommentsForReviews = _productService.GetCommentForProductReview(productID);
            prvm.Dislikes = _productService.GetAllDislikes();
            prvm.Likes = _productService.GetAllLikes();
            return View(prvm);
        }

        public IActionResult CreateRating(ProductAndReviewViewModel prvm)
        {
            var userID = int.Parse(_userManager.GetUserId(HttpContext.User));

            // 1 means like
            if (prvm.RatingOption == 1)
            {
                Like like = prvm.LikeForm;
                like.UserID = userID;
                var createdLike = _productService.CreateRating(1, like);
                if (createdLike)
                {
                    return RedirectToAction("Details", "Product", new { productID = prvm.Product.ProductID });
                }
                return RedirectToAction("Details", "Product", new { productID = prvm.Product.ProductID });
            }
            else
            {
                Dislike dislike = prvm.DislikeForm;
                dislike.UserID = userID;
                var createdDislike = _productService.CreateRating(2, null, dislike);
                if (createdDislike)
                {
                    return RedirectToAction("Details", "Product", new {productID = prvm.Product.ProductID});
                }
                return RedirectToAction("Details", "Product", new { productID = prvm.Product.ProductID });

            }
        }

        [HttpGet]
        public IActionResult Update(int productID)
        {
            var product = _productService.GetProductByID(productID);
            var pcvm = new ProductAndCategoryViewModel();
            pcvm.Product = product;
            pcvm.Categories = _productService.GetAllCategories();
            return View(pcvm);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            bool updatedProduct = _productService.Update(product);
            if (updatedProduct)
            {
                return RedirectToAction("Index", "Product");
            }
            return View(product);
        }

        public IActionResult UpdateCompanyProducts(string companyID)
        {
            var productsFromCompany = _productService.GetProductsByCompanyID(Convert.ToInt32(companyID));
            return View(productsFromCompany);
        }
    }
}
