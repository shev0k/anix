using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AniX_Controllers;
using Anix_Shared.DomainModels;
using AniX_Shared.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AniX_WEB.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly IUserManagement _userController;
        private readonly ISessionService _sessionService;

        public User CurrentUser { get; set; }
        public bool IsOwnProfile { get; set; } // To check if the user is viewing their own profile

        public ProfileModel(IUserManagement userController, ISessionService sessionService)
        {
            _userController = userController;
            _sessionService = sessionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_sessionService.IsAuthenticated())
            {
                return RedirectToPage("/Index");
            }

            int userIdFromSession = int.Parse(_sessionService.GetUserId());
            CurrentUser = await _userController.GetUserFromIdAsync(userIdFromSession);

            if (CurrentUser == null)
            {
                return RedirectToPage("/Error");
            }

            IsOwnProfile = User.FindFirstValue(ClaimTypes.NameIdentifier) == CurrentUser.Id.ToString();

            CurrentUser.ProfileImagePath = CurrentUser.ProfileImagePath ?? "https://anix.blob.core.windows.net/anixprofile/66574.png";

            return Page();
        }
    }
}