namespace IdentityEcommerce.Models.Repositories
{
    public interface IReviewRepository
    {
        bool Create(Review review);
        Product GetProductByID(int productID);
    }
}
