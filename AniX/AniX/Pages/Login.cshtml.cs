using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AniX_Shared.Interfaces;
using AniX_Shared.DomainModels;
using System;
using System.Threading.Tasks;
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
            Console.WriteLine("Session UserId: " + _sessionService.GetUserId()); // Assuming you have a GetUserId() method in your SessionService
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

            if (!ModelState.IsValid)
            {
                Message = "Please fill in all fields.";
                return Page();
            }

            if (string.IsNullOrEmpty(Username) || Username.Contains("--") ||
                string.IsNullOrEmpty(Password) || Password.Contains("--"))
            {
                Message = "Invalid input.";
                return Page();
            }

            User user = await _authenticationService.AuthenticateUserAsync(Username, Password);

            if (user == null || user.Banned)
            {
                Message = "Authentication failed. Please check your details or contact support if your account is banned.";
                // Log this attempt
                return Page();
            }

            Console.WriteLine("User successfully logged in with ID: " + user.Id);

            string sessionId = Guid.NewGuid().ToString();
            _authenticationService.SetWebUserSession(user);
            _sessionService.SetSessionAndCookie(user.Id.ToString(), user.Username, sessionId);
            return RedirectToPage("/Index");
        }
    }
}
