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
            return _context.Products.ToList();
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
    }
}
