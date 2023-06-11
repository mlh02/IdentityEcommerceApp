using IdentityApp.Models.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IdentityEcommerce.Models.ViewModels
{
    public class ProductAndReviewViewModel
    {
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Comment> CommentsForReviews { get; set; }
        public List<Like> Likes { get; set; }
        public List<Dislike> Dislikes { get; set; }

        // These are for form
        public Like LikeForm { get; set; }
        public Dislike DislikeForm { get; set; }
        public int RatingOption { get; set; }

        public int CalculateAverageRating()
        {
            if(Reviews.Count > 0)
            {
                var averageRating = Reviews.Sum(x => x.Rating) / Reviews.Count();
                return averageRating;
            }
            return 0;
        }

    }
}
