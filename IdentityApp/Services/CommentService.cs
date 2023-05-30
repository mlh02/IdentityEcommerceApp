using IdentityApp.Models.Repositories;
using IdentityEcommerce.Models;

namespace IdentityApp.Services
{
    public class CommentService
    {
        private readonly CommentRepository _commentRepos;

        public CommentService(CommentRepository commentRepos)
        {
            _commentRepos = commentRepos;
        }

        public bool Create(Comment comment)
        {
            bool createdComment = _commentRepos.Create(comment);
            return createdComment;
        }
    }
}
