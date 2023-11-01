using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using AniX_Controllers;
using AniX_Shared.DomainModels;
using AniX_Shared.Interfaces;
using System.Threading.Tasks;
using System.IO;
using Anix_Shared.DomainModels;
using AniX_Utility;

namespace AniX_WEB.Pages
{
    public class ProfileEditModel : PageModel
    {
        private readonly UserController _userController;
        private readonly ISessionService _sessionService;
        private readonly FTPService _ftpService;

        [BindProperty]
        public User EditableUser { get; set; }

        [BindProperty]
        public IFormFile ProfileImage { get; set; }

        public ProfileEditModel(UserController userController, ISessionService sessionService, FTPService ftpService)
        {
            _userController = userController;
            _sessionService = sessionService;
            _ftpService = ftpService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_sessionService.IsAuthenticated())
            {
                return RedirectToPage("/Index");
            }

            int userIdFromSession = int.Parse(_sessionService.GetUserId());
            EditableUser = await _userController.GetUserByIdAsync(userIdFromSession);

            if (EditableUser == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ProfileImage != null)
            {
                string uniqueFileName = $"{EditableUser.Id}_{ProfileImage.FileName}";
                string remoteFilePath = $"wwwroot/ProfileImages/{uniqueFileName}";

                using (Stream fileStream = ProfileImage.OpenReadStream())
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        await _ftpService.UploadFileAsync(reader.ReadToEnd(), remoteFilePath);
                    }
                }

                EditableUser.ProfileImagePath = remoteFilePath;
                await _userController.UpdateProfileImagePathAsync(EditableUser.Id, remoteFilePath);
                _sessionService.SetProfileImagePath(remoteFilePath);
            }

            // Update user details
            bool updateResult = await _userController.UpdateAsync(EditableUser);
            if (updateResult)
            {
                return RedirectToPage("/Profile");
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}