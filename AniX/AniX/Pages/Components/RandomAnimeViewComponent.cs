using AniX_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AniX_WEB.Pages.Components;

public class RandomAnimeViewComponent : ViewComponent
{
    private readonly IAnimeManagement _animeManagement;

    public RandomAnimeViewComponent(IAnimeManagement animeManagement)
    {
        _animeManagement = animeManagement;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var anime = await _animeManagement.GetRandomAnimeAsync();
        return View(anime);
    }
}