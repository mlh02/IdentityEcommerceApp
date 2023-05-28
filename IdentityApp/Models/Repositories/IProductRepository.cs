using System.Collections.Generic;

namespace IdentityEcommerce.Models.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        bool Create(Product product);
        Product GetProductByID(int productID);
        List<Review> GetReviewsOfSpecificProduct(int productID);
    }
}
