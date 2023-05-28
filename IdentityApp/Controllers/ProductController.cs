using IdentityEcommerce.Helpers.Enums;
using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Repositories;
using IdentityEcommerce.Models.ViewModels;
using IdentityEcommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace IdentityEcommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            if (User.IsInRole(AppRoleEnum.User.ToString()))
            {
                var allProducts = _productService.GetAllProducts();
                return View(allProducts);
            }
            return RedirectToAction("Login", "AppUser");

        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
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
            return View(prvm);
        }
    }
}
