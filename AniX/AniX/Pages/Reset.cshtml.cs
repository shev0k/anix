using AniX_BusinessLogic;
using AniX_Shared.Interfaces;
using AniX_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AniX_WEB.Pages
{
    public class ResetModel : PageModel
    {
        private readonly IUserManagement _userManagement;
        private readonly ISessionService _sessionService;

        [BindProperty]
        public ResetInputModel Input { get; set; }

        public ResetModel(IUserManagement userManagement, ISessionService sessionService)
        {
            _userManagement = userManagement;
            _sessionService = sessionService;
        }

        public class ResetInputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(20, MinimumLength = 6)]
            public string NewPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet()
        {
            Console.WriteLine("Session UserId: " + _sessionService.GetUserId());
            Console.WriteLine("IsAuthenticated: " + User.Identity.IsAuthenticated);

            if (_sessionService.IsAuthenticated())
            { 
                Response.Redirect("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManagement.GetUserByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User with this email does not exist.");
                return Page();
            }

            string newSalt = HashPassword.GenerateSalt();
            string hashedPassword = HashPassword.GenerateHashedPassword(Input.NewPassword, newSalt);
            user.UpdatePassword(hashedPassword, newSalt);

            var updateResult = await _userManagement.UpdateAsync(user, false);
            if (!updateResult)
            {
                ModelState.AddModelError("", "An error occurred while resetting the password.");
                return Page();
            }

            return RedirectToPage("/Login");
        }
    }
}
