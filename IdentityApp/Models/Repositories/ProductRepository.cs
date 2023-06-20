using IdentityApp.Migrations;
using IdentityApp.Models.Repositories;
using IdentityEcommerce.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IdentityEcommerce.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _context.Products.ToList();
            return products;
        }

        public Product GetProductByID(int productID)
        {
            var currentProduct = _context.Products.SingleOrDefault(x => x.ProductID == productID);
            return currentProduct;
        }

        public List<Review> GetReviewsOfSpecificProduct(int productID)
        {
            var reviews = _context.Reviews.Where(x => x.ProductID == productID).ToList();
            return reviews;
        }

        public List<Category> GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        public List<Comment> GetCommentForProductReview(int productID)
        {
            var specificComment = _context.Comments.Where(x => x.ProductID == productID).ToList();
            return specificComment;
        }

        public bool CreateRating(int ratingOption, Like like, Dislike dislike)
        {
            try
            {
                if(ratingOption == 1)
                {
                    _context.Likes.Add(like);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    _context.Dislikes.Add(dislike);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public List<Like> GetAllLikes()
        {
            var likes = _context.Likes.ToList();
            return likes;
        }

        public List<Dislike> GetAllDislikes()
        {
            var dislikes = _context.Dislikes.ToList();
            return dislikes;
        }

        public bool Update(Product product)
        {
            try
            {
                _context.Products.Update(product);
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
