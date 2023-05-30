using IdentityEcommerce.Models;

namespace IdentityApp.Models.Repositories
{
    public interface ICommentRepository
    {
        bool Create(Comment comment);
    }
}
