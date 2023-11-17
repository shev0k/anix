using AniX_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace AniX_WEB.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly ISessionService _sessionService;

        public LogoutModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public IActionResult OnGet()
        {
            _sessionService.SignOut();

            Response.Cookies.Delete("SessionID");
            TempData.Clear();
            return RedirectToPage("/Index");
        }
    }

}
