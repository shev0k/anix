using AniX_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AniX_Shared.Extensions;
using AniX_Shared.DomainModels;
using AniX_Utility;

namespace AniX_WEB.Pages
{
    public class AnimeDetailModel : PageModel
    {
        private readonly IAnimeManagement _animeManagement;
        private readonly IReviewManagement _reviewManagement;
        private readonly IUserManagement _userManagement;
        private readonly ISessionService _sessionService;
        private readonly IUserAnimeActionManagement _userAnimeActionManagement;

        public AniX_Shared.Extensions.AnimeDetailModel AnimeDetails { get; set; }
        public List<AnimeWithRatings> RecommendedAnimes { get; set; }

        [BindProperty]
        public string ReviewText { get; set; }

        [BindProperty]
        public int StarRating { get; set; }

        public bool IsAnimeInWatchlist { get; private set; }
        public bool IsAnimeInPlaylist { get; private set; }


        public AnimeDetailModel(IAnimeManagement animeManagement, IReviewManagement reviewManagement, IUserManagement userManagement, ISessionService sessionService, IUserAnimeActionManagement userAnimeActionManagement)
        {
            _animeManagement = animeManagement;
            _reviewManagement = reviewManagement;
            _userManagement = userManagement;
            _sessionService = sessionService;
            _userAnimeActionManagement = userAnimeActionManagement;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var currentUserId = User.Identity.IsAuthenticated
                ? int.Parse(_sessionService.GetUserId())
                : (int?)null;

            AnimeDetails = await _animeManagement.GetAnimeDetailAsync(id, currentUserId);

            if (AnimeDetails == null || AnimeDetails.Anime == null)
            {
                Console.WriteLine("AnimeDetails or Anime is null.");
                return RedirectToPage("/404");
            }


            var animeView = new AnimeViews
            {
                AnimeId = id,
                UserId = currentUserId,
                ViewDate = DateTime.UtcNow 
            };
            await _animeManagement.RecordAnimeViewAsync(animeView);

            RecommendedAnimes = await _animeManagement.GetRecommendedAnimesAsync(null, id);

            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(_sessionService.GetUserId());
                IsAnimeInWatchlist = await _userAnimeActionManagement.IsAnimeInUserWatchlist(userId, id);
                IsAnimeInPlaylist = await _userAnimeActionManagement.IsAnimeInUserPlaylist(userId, id);
            }

            Console.WriteLine("Session UserId: " + _sessionService.GetUserId());
            Console.WriteLine("IsAuthenticated: " + User.Identity.IsAuthenticated);
            return Page();
        }

        public async Task<IActionResult> OnPostAddToWatchlistAsync(int animeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { success = true, message = "Please log in to add to watchlist." });
            }

            var userId = int.Parse(_sessionService.GetUserId());
            var action = new WatchLater
            {
                UserId = userId,
                Anime = new Anime { Id = animeId }
            };

            var result = await _userAnimeActionManagement.AddUserAnimeActionAsync(action);

            return new JsonResult(new { success = result.Success, message = result.Message });
        }

        public async Task<IActionResult> OnPostRemoveFromWatchlistAsync(int animeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { success = false, message = "Please log in." });
            }

            var userId = int.Parse(_sessionService.GetUserId());
            var action = new WatchLater
            {
                UserId = userId,
                Anime = new Anime { Id = animeId }
            };

            var result = await _userAnimeActionManagement.RemoveUserAnimeActionAsync(action);

            return new JsonResult(new { success = result.Success, message = result.Message });
        }

        public async Task<IActionResult> OnPostAddToPlaylistAsync(int animeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { success = true, message = "Please log in to add to playlist." });
            }

            var userId = int.Parse(_sessionService.GetUserId());
            var action = new PlaylistItem
            {
                UserId = userId,
                Anime = new Anime { Id = animeId }
            };

            var result = await _userAnimeActionManagement.AddUserAnimeActionAsync(action);

            return new JsonResult(new { success = result.Success, message = result.Message });
        }

        public async Task<IActionResult> OnPostRemoveFromPlaylistAsync(int animeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { success = false, message = "Please log in." });
            }

            var userId = int.Parse(_sessionService.GetUserId());
            var action = new PlaylistItem
            {
                UserId = userId,
                Anime = new Anime { Id = animeId }
            };

            var result = await _userAnimeActionManagement.RemoveUserAnimeActionAsync(action);

            return new JsonResult(new { success = result.Success, message = result.Message });
        }

        public bool IsInWatchlist(int animeId)
        {
            return IsAnimeInWatchlist;
        }

        public bool IsInPlaylist(int animeId)
        {
            return IsAnimeInPlaylist;
        }

        public async Task<IActionResult> OnPostUpdateRatingAsync(int animeId, double newRating)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new JsonResult(new { success = false, message = "Please log in." });
            }

            var userId = int.Parse(_sessionService.GetUserId());
            var existingReviews = await _reviewManagement.GetReviewsByUserIdAndAnimeIdAsync(userId, animeId);

            if (existingReviews == null || !existingReviews.Any())
            {
                return new JsonResult(new { success = false, message = "Please write a review first." });
            }

            var actualRating = newRating * 2;


            var result = await _reviewManagement.UpdateReviewRatingAsync(userId, animeId, actualRating);

            return new JsonResult(new { success = result.Success, message = result.Message });
        }

        public async Task<IActionResult> OnPostDeleteReviewAsync(int reviewId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            var userId = int.Parse(_sessionService.GetUserId());
            var review = await _reviewManagement.GetReviewByIdAsync(reviewId);
            if (review == null || review.UserId != userId)
            {
                return Page();
            }

            var result = await _reviewManagement.DeleteReviewAsync(reviewId);
            if (!result.Success)
            {
                TempData["Message"] = "An error occurred while deleting the review.";
                TempData["MessageType"] = "error";
            }
            else
            {
                TempData["Message"] = "Review deleted successfully.";
                TempData["MessageType"] = "success";

                AnimeDetails = await _animeManagement.GetAnimeDetailAsync(review.AnimeId, userId);
                RecommendedAnimes = await _animeManagement.GetRecommendedAnimesAsync(userId, review.AnimeId);
                if (AnimeDetails == null || AnimeDetails.Anime == null)
                {
                    return RedirectToPage("/404");
                }
            }

            return RedirectToPage(new { id = review.AnimeId });
        }

        public async Task<IActionResult> OnPostPostReviewAsync(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            var userId = _sessionService.GetUserId();
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Page();
            }

            if (string.IsNullOrWhiteSpace(ReviewText))
            {
                return Page();
            }

            var actualRating = StarRating * 2;

            var newReview = new AniX_Shared.DomainModels.Review
            {
                UserId = int.Parse(userId),
                AnimeId = id,
                Text = ReviewText,
                Rating = actualRating,
                IsApproved = false
            };



            var result = await _reviewManagement.CreateReviewAsync(newReview);
            if (!result.Success)
            {
                TempData["Message"] = "An error occurred while submitting the review.";
            }
            else
            {
                await _reviewManagement.UpdateReviewRatingAsync(int.Parse(userId), id, actualRating);
                TempData["Message"] = "Review submitted successfully.";
                TempData["MessageType"] = "success";
            }

            return RedirectToPage(new { id = id });
        }

    }
}
