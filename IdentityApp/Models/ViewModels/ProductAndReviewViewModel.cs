using IdentityApp.Models.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IdentityEcommerce.Models.ViewModels
{
    public class ProductAndReviewViewModel
    {
        public Product Product { get; set; }
        public AppUser CurrentUser { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Comment> CommentsForReviews { get; set; }
        public List<Like> Likes { get; set; }
        public List<Dislike> Dislikes { get; set; }
        public List<Company> Companies { get; set; }
        public List<Category> Categories { get; set; }

        // These are for form
        public Like LikeForm { get; set; }
        public Dislike DislikeForm { get; set; }
        public Comment CommentForm { get; set; }
        public Review ReviewForm { get; set; }

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
        public Dictionary<int, double> CalculateRatingPercentage(List<Review> reviews)
        {
            var ratingCounts = new Dictionary<int, int>();
            var totalReviews = reviews.Count;

            // Initialize the rating counts dictionary with 0 for each rating
            for (int i = 1; i <= 5; i++)
            {
                ratingCounts[i] = 0;
            }

            // Count the occurrences of each rating
            foreach (var review in reviews)
            {
                ratingCounts[review.Rating]++;
            }

            var ratingPercentages = new Dictionary<int, double>();

            // Calculate the percentage of each rating
            foreach (var kvp in ratingCounts)
            {
                var rating = kvp.Key;
                var count = kvp.Value;
                var percentage = (count / (double)totalReviews) * 100;
                ratingPercentages[rating] = percentage;
            }

            return ratingPercentages;
        }


    }
}
