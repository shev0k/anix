using Microsoft.AspNetCore.Mvc;

namespace AniX_WEB.Pages.Components;

public class TypesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var types = new List<string> { "Movie", "TV", "OVA", "ONA", "Special", "Music" };
        return View(types);
    }
}