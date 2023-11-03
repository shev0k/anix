using AniX_BusinessLogic;
using Anix_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AniX_WEB.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISessionService _sessionService;
        private readonly UserValidationService _userValidationService;
        public RegisterModel(IAuthenticationService authenticationService, ISessionService sessionService)
        {
            _authenticationService = authenticationService;
            _sessionService = sessionService;
            _userValidationService = new UserValidationService(minUsernameLength: 4, minPasswordLength: 4, passwordRegexPattern: @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,64}$");
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            if (_sessionService.IsAuthenticated())
            {
                Response.Redirect("/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_sessionService.IsAuthenticated())
            {
                return RedirectToPage("/Index");
            }

            if (!ModelState.IsValid)
            {
                Message = "Please fill in all fields.";
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                Message = "Passwords do not match.";
                return Page();
            }

            string validationMessage;
            if (!_userValidationService.ValidateCredentials(Username, Password, out validationMessage))
            {
                Message = validationMessage;
                return Page();
            }

            //if (!_userValidationService.ValidatePasswordComplexity(Password))
            //{
            //    Message = "Password must contain at least one upper case, one lower case, and one number.";
            //    return Page();
            //}

            OperationResult registrationResult = await _authenticationService.RegisterUserAsync(Username, Email, Password);

            if (registrationResult.Success)
            {
                string sessionId = Guid.NewGuid().ToString();

                User registeredUser = await _authenticationService.AuthenticateUserAsync(Username, Password);

                if (registeredUser != null)
                {
                    _sessionService.SetSessionAndCookie(registeredUser.Id.ToString(), Username, sessionId, registeredUser.ProfileImagePath);
                    return RedirectToPage("/Index");
                }
                else
                {
                    Message = "Registration was successful, but an error occurred while logging you in.";
                    return Page();
                }
            }
            else
            {

                if (await _authenticationService.DoesUsernameExistAsync(Username))
                {
                    Message = "Username already exists. Please choose another.";
                }
                else if (await _authenticationService.DoesEmailExistAsync(Email))
                {
                    Message = "Email already exists. Please use another.";
                }
                else
                {
                    Message = "Registration failed. Please try again later.";
                }
                return Page();
            }
        }

        //private bool IsValidEmail(string email)
        //{
        //    // for later
        //    return true;
        //}

        //private bool IsValidPassword(string password)
        //{
        //    // for later
        //    return true;
        //}
    }
}
