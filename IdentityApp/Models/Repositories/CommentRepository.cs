using IdentityEcommerce.Data;
using IdentityEcommerce.Models;

namespace IdentityApp.Models.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(Comment comment)
        {
            try
            {
                _context.Comments.Add(comment);
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
