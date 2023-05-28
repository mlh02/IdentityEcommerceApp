using IdentityEcommerce.Models;
using IdentityEcommerce.Models.Repositories;

namespace IdentityEcommerce.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepos;

        public ReviewService(ReviewRepository reviewRepos)
        {
            _reviewRepos = reviewRepos;
        }

        public bool Create(Review review)
        {
            bool createdReview = _reviewRepos.Create(review);
            return createdReview;
        }

        public Product GetProductByID(int productID)
        {
            Product currentProduct = _reviewRepos.GetProductByID(productID);
            return currentProduct;
        }
    }
}
