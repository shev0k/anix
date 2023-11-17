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
using System.Text.RegularExpressions;

namespace AniX_WEB.Pages
{
    public class ProfileEditModel : PageModel
    {
        private readonly IUserManagement _userManagement;
        private readonly ISessionService _sessionService;
        private readonly IAzureBlobService _azureBlobService;

        private const int MinUsernameLength = 4;
        private const int MinPasswordLength = 4;
        private const string PasswordRegexPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$";


        [BindProperty]
        public ProfileEditInputModel Input { get; set; }

        [TempData]
        public string Message { get; set; } 

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

            [StringLength(20, MinimumLength = 4)]
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

            TempData.Clear();

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
                if (Input.Username.Length < MinUsernameLength)
                {
                    TempData["Message"] = $"Username must be at least {MinUsernameLength} characters long.";
                    Input.ProfileImagePath = user.ProfileImagePath;
                    return Page();
                }
                if (await _userManagement.DoesUsernameExistAsync(Input.Username))
                {
                    TempData["Message"] = "Username already exists.";
                    Input.ProfileImagePath = user.ProfileImagePath;
                    return Page();
                }
                user.Username = Input.Username;
                updateUser = true;
            }

            if (!string.IsNullOrWhiteSpace(Input.Email) && Input.Email != user.Email)
            {
                if (await _userManagement.DoesEmailExistAsync(Input.Email))
                {
                    TempData["Message"] = "Email already exists.";
                    Input.ProfileImagePath = user.ProfileImagePath;
                    return Page();
                }
                user.Email = Input.Email;
                updateUser = true;
            }

            if (!string.IsNullOrWhiteSpace(Input.Password))
            {
                if (Input.Password.Length < MinPasswordLength)
                {
                    TempData["Message"] = $"Password must be at least {MinPasswordLength} characters long.";
                    Input.ProfileImagePath = user.ProfileImagePath;
                    return Page();
                }
                //if (!Regex.IsMatch(Input.Password, PasswordRegexPattern))
                //{
                //      TempData["Message"] = "Password must contain at least one uppercase letter, one lowercase letter, and one number.";
                //      return Page();
                //}
                // Continue with the password update logic...
                string newSalt = HashPassword.GenerateSalt();
                string hashedPassword = HashPassword.GenerateHashedPassword(Input.Password, newSalt);
                user.UpdatePassword(hashedPassword, newSalt);
                updateUser = true;
            }

            if (Input.ProfileImage != null && Input.ProfileImage.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(Input.ProfileImage.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    TempData["Message"] = "JPG, JPEG, PNG and GIF only!";
                    Input.ProfileImagePath = user.ProfileImagePath;
                    return Page();
                }

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
                var updateSuccess = await _userManagement.UpdateAsync(user, updateProfileImage);
                if (!updateSuccess)
                {
                    TempData["ErrorMessage"] = "An error occurred while updating the profile.";
                    return Page();
                }
                else
                {
                    _sessionService.SetUsername(user.Username);
                    _sessionService.SetProfileImagePath(user.ProfileImagePath);
                    return RedirectToPage("/Profile");
                }
            }


            Input = new ProfileEditInputModel
            {
                Username = user.Username,
                Email = user.Email,
                ProfileImagePath = user.ProfileImagePath
            };

            return RedirectToPage("/Profile");
        }

    }
}
