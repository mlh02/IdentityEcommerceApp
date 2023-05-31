using IdentityApp.Models.Repositories;
using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public List<Category> GetAllCategories()
        {
            var categories = _productRepos.GetAllCategories();
            return categories;
        }

        public List<Comment> GetCommentForProductReview(int productID)
        {
            var specificComment = _productRepos.GetCommentForProductReview(productID);
            return specificComment;

        }

        public bool CreateRating(int ratingOption, Like like = null, Dislike dislike = null)
        {
            var likes = GetAllLikes();
            var dislikes = GetAllDislikes();
            if(ratingOption == 1)
            {
                if (likes.Any(x => x.UserID == like.UserID && x.ReviewID == like.ReviewID) || dislikes.Any(x => x.UserID == like.UserID && x.ReviewID == like.ReviewID)){
                    return false;
                }
            }
            if(ratingOption == 2)
            {
                if (dislikes.Any(x => x.UserID == dislike.UserID && x.ReviewID == dislike.ReviewID) || likes.Any(x => x.UserID == dislike.UserID && x.ReviewID == dislike.ReviewID))
                {
                    return false;
                }
            }
            var addedRating = _productRepos.CreateRating(ratingOption, like, dislike);
            return addedRating;
        }

        public List<Like> GetAllLikes()
        {
            var likes = _productRepos.GetAllLikes();
            return likes;
        }

        public List<Dislike> GetAllDislikes()
        {
            var dislikes = _productRepos.GetAllDislikes();
            return dislikes;
        }
    }
}
