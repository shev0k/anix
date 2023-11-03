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
        private readonly string _defaultImagePath;

        public User CurrentUser { get; set; }
        public bool IsOwnProfile { get; set; }

        public ProfileModel(IUserManagement userController, ISessionService sessionService, IConfiguration configuration)
        {
            _userController = userController;
            _sessionService = sessionService;
            _defaultImagePath = configuration["ProfileDefaults:ImagePath"];
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

            CurrentUser.ProfileImagePath = CurrentUser.ProfileImagePath ?? _defaultImagePath;

            return Page();
        }
    }
}