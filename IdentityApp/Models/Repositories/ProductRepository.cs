using IdentityApp.Models.Repositories;
using IdentityApp.Models.ViewModels;
using IdentityEcommerce.Data;
using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<Product> GetAllProductsArchived()
        {
            var products = _context.Products
                    .Include(x => x.Category)
                        .Include(x => x.Company)
                          .Where(x =>  x.Archived)
                             .ToList();
            return products;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            var products = _context.Products
                    .Include(x => x.Category)
                        .Include(x => x.Company)
                          .Where(x => !x.Archived)
                             .ToList();
            return products;
        }
        public ProductIndexViewModel GetIndexViewModel(int categoryId, int companyId)
        {
            ProductIndexViewModel pac = new ProductIndexViewModel();
            pac.Categories = _context.Categories.ToList();
            pac.Companies = _context.Companies.ToList();
            if(categoryId == 0 && companyId == 0)
            {
                pac.Products = _context.Products.Where(x =>   x.Archived == false).ToList();
            }
            if (categoryId != 0 && companyId == 0)
            {
                pac.Products = _context.Products.Where(x => x.CategoryID == categoryId && x.Archived == false).ToList();

            }
            if (categoryId == 0 && companyId != 0)
            {
                pac.Products = _context.Products.Where(x => x.CompanyID == companyId && x.Archived == false).ToList();

            }
            pac.Reviews = _context.Reviews.ToList();
            return pac;
        }

        public Product GetProductByID(int productID)
        {
            var currentProduct = _context.Products
                .Include(x => x.Company)
                   .Include(x => x.Category)
                        .SingleOrDefault(x => x.ProductID == productID);
            return currentProduct;
        }

        public List<Review> GetReviewsOfSpecificProduct(int productID)
        {
            var reviews = _context.Reviews
                    .Include(x => x.AppUser)
                        .Where(x => x.ProductID == productID).ToList();
            return reviews;
        }

        public List<Category> GetAllCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        public List<Comment> GetCommentForProductReview(int productID)
        {
            var specificComment = _context.Comments
                        .Where(x => x.ProductID == productID).ToList();
            
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
        public List<Company> GetAllCompanies()
        {
            var companies = _context.Companies.ToList();
            return companies;
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
