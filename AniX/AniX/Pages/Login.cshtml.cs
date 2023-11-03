using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AniX_Shared.Interfaces;
using AniX_Shared.DomainModels;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Principal;
using Anix_Shared.DomainModels;

namespace AniX_WEB.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISessionService _sessionService;

        public LoginModel(IAuthenticationService authenticationService, ISessionService sessionService)
        {
            _authenticationService = authenticationService;
            _sessionService = sessionService;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            Console.WriteLine("Session UserId: " + _sessionService.GetUserId());
            Console.WriteLine("IsAuthenticated: " + User.Identity.IsAuthenticated);

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

            if (!IsInputValid(out string validationMessage))
            {
                Message = validationMessage;
                return Page();
            }

            User user = await _authenticationService.AuthenticateUserAsync(Username, Password);

            if (user == null)
            {
                Message = "Authentication failed. Please check your credentials.";
                return Page();
            }

            if (user.Banned)
            {
                Message = "Your account has been banned. Please contact support for assistance.";
                return Page();
            }

            _sessionService.SetSessionAndCookie(user.Id.ToString(), user.Username, Guid.NewGuid().ToString(), user.ProfileImagePath);

            // No need to check the session immediately after setting it
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            }, "CustomAuthScheme");

            HttpContext.User = new ClaimsPrincipal(identity);

            Console.WriteLine("Session UserId: " + _sessionService.GetUserId());
            Console.WriteLine("IsAuthenticated: " + User.Identity.IsAuthenticated);

            return RedirectToPage("/Index");
        }



        private bool IsInputValid(out string validationMessage)
        {
            if (!ModelState.IsValid)
            {
                validationMessage = "Please fill in all fields.";
                return false;
            }

            const string invalidInputPattern = "--";
            if (string.IsNullOrEmpty(Username) || Username.Contains(invalidInputPattern) ||
                string.IsNullOrEmpty(Password) || Password.Contains(invalidInputPattern))
            {
                validationMessage = "Invalid input.";
                return false;
            }

            validationMessage = string.Empty;
            return true;
        }

        public IActionResult OnGetPostLogin()
        {
            Console.WriteLine("Session UserId: " + _sessionService.GetUserId());
            Console.WriteLine("IsAuthenticated: " + User.Identity.IsAuthenticated);
            return RedirectToPage("/Index");
        }
    }
}
