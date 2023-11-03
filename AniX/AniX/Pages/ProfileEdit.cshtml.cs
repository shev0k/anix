using AniX_Shared.Interfaces;
using AniX_Shared.DomainModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using AniX_Utility;
using System;

namespace AniX_WEB.Pages
{
    public class ProfileEditModel : PageModel
    {
        private readonly IUserManagement _userManagement;
        private readonly ISessionService _sessionService;
        private readonly IAzureBlobService _azureBlobService;

        [BindProperty]
        public ProfileEditInputModel Input { get; set; }

        public ProfileEditModel(IUserManagement userManagement, ISessionService sessionService, IAzureBlobService azureBlobService)
        {
            _userManagement = userManagement;
            _sessionService = sessionService;
            _azureBlobService = azureBlobService;
        }

        public class ProfileEditInputModel
        {
            [EmailAddress]
            [Display(Name = "Email Address")]
            public string Email { get; set; }

            [StringLength(100, MinimumLength = 3)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "New Password")]
            public string Password { get; set; }

            public string ProfileImagePath { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "Profile Image")]
            public IFormFile ProfileImage { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = _sessionService.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            var user = await _userManagement.GetUserFromIdAsync(int.Parse(userId));
            if (user == null)
            {
                return NotFound("User not found.");
            }

            Input = new ProfileEditInputModel
            {
                Username = user.Username,
                Email = user.Email,
                ProfileImagePath = user.ProfileImagePath
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = _sessionService.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            var user = await _userManagement.GetUserFromIdAsync(int.Parse(userId));
            if (user == null)
            {
                return NotFound("User not found.");
            }

            bool updateUser = false;
            bool updateProfileImage = false;

            if (!string.IsNullOrWhiteSpace(Input.Username) && Input.Username != user.Username)
            {
                if (await _userManagement.DoesUsernameExistAsync(Input.Username))
                {
                    ModelState.AddModelError("", "Username already exists.");
                    return Page();
                }
                user.Username = Input.Username;
                updateUser = true;
            }

            if (!string.IsNullOrWhiteSpace(Input.Email) && Input.Email != user.Email)
            {
                if (await _userManagement.DoesEmailExistAsync(Input.Email))
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return Page();
                }
                user.Email = Input.Email;
                updateUser = true;
            }

            if (!string.IsNullOrWhiteSpace(Input.Password))
            {
                string newSalt = HashPassword.GenerateSalt();
                string hashedPassword = HashPassword.GenerateHashedPassword(Input.Password, newSalt);
                user.UpdatePassword(hashedPassword, newSalt);
                updateUser = true;
            }

            if (Input.ProfileImage != null && Input.ProfileImage.Length > 0)
            {
                using (var stream = Input.ProfileImage.OpenReadStream())
                {
                    var newProfileImagePath = await _azureBlobService.UploadImageAsync(stream, user.Id.ToString(), Input.ProfileImage.ContentType);

                    if (!string.IsNullOrWhiteSpace(user.ProfileImagePath))
                    {
                        await _azureBlobService.DeleteImageAsync(user.ProfileImagePath);
                    }

                    user.ProfileImagePath = newProfileImagePath;
                    updateUser = true;
                    updateProfileImage = true;
                }
            }

            if (updateUser)
            {
                // Pass the updateProfileImage flag to the UpdateAsync method
                var updateSuccess = await _userManagement.UpdateAsync(user, updateProfileImage);
                if (!updateSuccess)
                {
                    ModelState.AddModelError("", "An error occurred while updating the profile.");
                    return Page();
                }
            }

            TempData["SuccessMessage"] = "Profile updated successfully.";
            return RedirectToPage("/Profile");
        }
    }
}
