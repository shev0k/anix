using AniX_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AniX_Shared.Extensions;

namespace AniX_WEB.Pages
{
    public class AnimeDetailModel : PageModel
    {
        private readonly IAnimeManagement _animeManagement;
        public AniX_Shared.Extensions.AnimeDetailModel AnimeDetails { get; set; }


        public AnimeDetailModel(IAnimeManagement animeManagement)
        {
            _animeManagement = animeManagement;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            AnimeDetails = await _animeManagement.GetAnimeDetailAsync(id);
            if (AnimeDetails == null || AnimeDetails.Reviews == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
