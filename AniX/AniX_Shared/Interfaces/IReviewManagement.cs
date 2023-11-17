using AniX_Shared.DomainModels;
using AniX_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniX_Shared.Interfaces
{
    public interface IReviewManagement
    {
        Task<OperationResult> CreateReviewAsync(Review review);
        Task<OperationResult> UpdateReviewRatingAsync(int userId, int animeId, double newRating);
        Task<OperationResult> DeleteReviewAsync(int reviewId);
        Task<OperationResult> ApproveReviewAsync(int reviewId);
        Task<List<Review>> GetReviewsByUserIdAndAnimeIdAsync(int userId, int animeId);
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task<List<Review>> GetPendingReviewsAsync();
        Task<List<Review>> GetApprovedReviewsByAnimeIdAsync(int animeId);
        Task<List<Review>> FetchFilteredReviewsAsync(string filter);
    }
}
