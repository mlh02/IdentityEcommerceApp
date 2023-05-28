using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IdentityEcommerce.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepos;

        public ProductService(ProductRepository productRepos)
        {
            _productRepos = productRepos;
        }

        public bool Create(Product product)
        {
            var random = new Random();
            product.RewardPoints = random.Next(10, 50);
            bool createdProduct = _productRepos.Create(product);
            return createdProduct;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var allProducts = _productRepos.GetAllProducts();
            return allProducts;
        }
        public Product GetProductByID(int productID)
        {
            var currentProduct = _productRepos.GetProductByID(productID);
            return currentProduct;
        }

        public List<Review> GetReviewsOfSpecificProduct(int productID)
        {
            var reviews = _productRepos.GetReviewsOfSpecificProduct(productID);
            return reviews;
        }

    }
}
