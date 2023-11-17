using AniX_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AniX_WEB.Pages.Components;

public class GenresViewComponent : ViewComponent
{
    private readonly IAnimeManagement _animeManagement;

    public GenresViewComponent(IAnimeManagement animeManagement)
    {
        _animeManagement = animeManagement;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var genres = await _animeManagement.GetAllGenresAsync();
        return View(genres);
    }
}