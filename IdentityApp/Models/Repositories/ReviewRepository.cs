using IdentityEcommerce.Data;
using System.Linq;

namespace IdentityEcommerce.Models.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Review review)
        {
            try
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public Product GetProductByID(int productID)
        {
            var currentProduct = _context.Products.SingleOrDefault(x => x.ProductID == productID);
            return currentProduct;
        }
    }
}
