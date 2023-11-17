using AniX_Controllers;
using Anix_Shared.DomainModels;
using AniX_Shared.DomainModels;
using AniX_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AniX_WEB.Pages
{
    public class PlaylistModel : PageModel
    {
        private readonly IUserAnimeActionManagement _userAnimeActionManagement;
        private readonly ISessionService _sessionService;
        private readonly IUserManagement _userManagement;

        public User CurrentUser { get; set; }
        public bool IsOwnProfile { get; set; }

        public List<WatchLater> WatchlistAnimes { get; set; }
        public List<PlaylistItem> PlaylistAnimes { get; set; }

        public PlaylistModel(IUserAnimeActionManagement userAnimeActionManagement,
            ISessionService sessionService,
            IUserManagement userManagement)
        {
            _userAnimeActionManagement = userAnimeActionManagement;
            _sessionService = sessionService;
            _userManagement = userManagement;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Login");
            }

            var userId = int.Parse(_sessionService.GetUserId());
            CurrentUser = await _userManagement.GetUserFromIdAsync(userId);
            if (CurrentUser == null)
            {
                return RedirectToPage("/404");
            }


            IsOwnProfile = User.FindFirstValue(ClaimTypes.NameIdentifier) == CurrentUser.Id.ToString();

            WatchlistAnimes = await _userAnimeActionManagement.GetUserWatchlistAsync(userId);
            PlaylistAnimes = await _userAnimeActionManagement.GetUserPlaylistAsync(userId);

            Console.WriteLine("Session UserId: " + _sessionService.GetUserId());
            Console.WriteLine("IsAuthenticated: " + User.Identity.IsAuthenticated);
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveFromWatchlistAsync(int animeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Please log in to remove from watchlist.";
                TempData["MessageType"] = "error";
                return RedirectToPage("/Login");
            }

            var userId = int.Parse(_sessionService.GetUserId());
            var action = new WatchLater
            {
                UserId = userId,
                Anime = new Anime { Id = animeId }
            };

            var result = await _userAnimeActionManagement.RemoveUserAnimeActionAsync(action);

            TempData["Message"] = result.Success ? "Anime removed from watchlist." : result.Message;
            TempData["MessageType"] = result.Success ? "success" : "error";

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveFromPlaylistAsync(int animeId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Message"] = "Please log in to remove from playlist.";
                TempData["MessageType"] = "error";
                return RedirectToPage("/Login");
            }

            var userId = int.Parse(_sessionService.GetUserId());
            var action = new PlaylistItem
            {
                UserId = userId,
                Anime = new Anime { Id = animeId }
            };

            var result = await _userAnimeActionManagement.RemoveUserAnimeActionAsync(action);

            TempData["Message"] = result.Success ? "Anime removed from playlist." : result.Message;
            TempData["MessageType"] = result.Success ? "success" : "error";

            return RedirectToPage();
        }
    }
}
