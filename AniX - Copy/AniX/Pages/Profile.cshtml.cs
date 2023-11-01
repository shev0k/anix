using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AniX_Controllers;
using Anix_Shared.DomainModels;
using AniX_Shared.Interfaces;
using System.Threading.Tasks;

namespace AniX_WEB.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly UserController _userController;
        private readonly ISessionService _sessionService;
        public string ProfileImageUrl { get; set; }

        public User CurrentUser { get; set; }

        public ProfileModel(UserController userController, ISessionService sessionService)
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

            CurrentUser = await _userController.GetUserByIdAsync(userIdFromSession);

            if (CurrentUser == null)
            {
                return RedirectToPage("/Error");
            }

            ProfileImageUrl = !string.IsNullOrEmpty(CurrentUser.ProfileImagePath) ?
                CurrentUser.ProfileImagePath :
                _userController.GetDefaultProfileImage();

            return Page();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userController.GetUserByIdAsync(userId);
        }
    }
}