using Anix_Shared.DomainModels;
using AniX_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AniX_WEB.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISessionService _sessionService;

        public RegisterModel(IAuthenticationService authenticationService, ISessionService sessionService)
        {
            _authenticationService = authenticationService;
            _sessionService = sessionService;
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

            // Validate username and email
            if (await _authenticationService.DoesUsernameExistAsync(Username))
            {
                Message = "Username already exists. Please choose another.";
                return Page();
            }

            if (await _authenticationService.DoesEmailExistAsync(Email))
            {
                Message = "Email already exists. Please use another.";
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                Message = "Passwords do not match.";
                return Page();
            }

            ////Custom validations for later
            //if (!IsValidEmail(Email))
            //    {
            //        Message = "Invalid email format.";
            //        return Page();
            //    }

            //if (!IsValidPassword(Password))
            //{
            //    Message = "Password must contain at least one upper case, one lower case, and one number.";
            //    return Page();
            //}

            bool registrationSuccess = await _authenticationService.RegisterUserAsync(Username, Email, Password);

            if (registrationSuccess)
            {
                string sessionId = Guid.NewGuid().ToString();

                User registeredUser = await _authenticationService.AuthenticateUserAsync(Username, Password);

                if (registeredUser != null)
                {
                    _sessionService.SetSessionAndCookie(registeredUser.Id.ToString(), Username, sessionId);
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
                Message = "Registration failed. Please try again.";
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
