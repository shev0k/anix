using Anix_Shared.DomainModels;
using AniX_Shared.DomainModels;
using AniX_Utility;


namespace AniX_FormsLogic
{
    public class ReviewsFormLogic
    {
        private ApplicationModel _appModel;

        public ReviewsFormLogic(ApplicationModel appModel)
        {
            _appModel = appModel;
        }

        public async Task<List<Review>> UpdateReviewListAsync(string selectedFilter)
        {
            return await _appModel.ReviewController.FetchFilteredReviewsAsync(selectedFilter);
        }

        public async Task<OperationResult> ApproveReviewAsync(int reviewId)
        {
            try
            {
                return await _appModel.ReviewController.ApproveReviewAsync(reviewId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<OperationResult> DeleteReviewAsync(int reviewId)
        {
            try
            {
                return await _appModel.ReviewController.DeleteReviewAsync(reviewId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string ShowReviewDetails(Review review)
        {
            return $"Name: {review.UserName}\nAnime: {review.AnimeName}\nRating: {review.Rating.Value:F2}\nText: {review.Text}\nApproved: {(review.IsApproved ? "Yes" : "No")}";
        }

        public Review GetSelectedReviewFromDataGridView(List<Tuple<Review, object>> originalReviews, int rowIndex)
        {
            return originalReviews[rowIndex].Item1;
        }

        public List<Tuple<Review, object>> TransformReviewsForDataGridView(List<Review> reviews)
        {
            return reviews.Select(r => new Tuple<Review, object>(
                r,
                new
                {
                    r.Id,
                    User = r.UserName,
                    Anime = r.AnimeName,
                    Rating = r.Rating.HasValue ? $"{r.Rating.Value:F2} Stars" : "No Rating",
                    Text = r.Text.Length > 50 ? r.Text.Substring(0, 47) + "..." : r.Text,
                    Approved = r.IsApproved ? "Yes" : "No"
                }
            )).ToList();
        }
    }
}
