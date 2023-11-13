using AniX_Shared.DomainModels;
using AniX_Shared.Extensions;
using AniX_Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AniX.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAnimeManagement _animeManagement;
        public List<AnimeWithRatings> FeaturedAnimes { get; set; }
        public List<AnimeWithRatings> RecentlyUpdatedAnimes { get; set; }
        public List<AnimeWithRatings> NewlyReleasedAnimes { get; set; }
        public AnimeWithRatings UpcomingAnime { get; set; }
        public List<AnimeWithPopularity> PopularAnimes { get; set; }
        public List<AnimeWithViewCount> TopWatchedAnimes { get; set; }
        public List<AnimeWithRatings> TopRatedAnime { get; set; }

        public IndexModel(IAnimeManagement animeManagement)
        {
            _animeManagement = animeManagement;
        }

        public async Task OnGetAsync()
        {
            FeaturedAnimes = await _animeManagement.GetRandomAnimesAsync(3);
            RecentlyUpdatedAnimes = await _animeManagement.GetRecentlyUpdatedAnimesAsync(8);
            NewlyReleasedAnimes = await _animeManagement.GetNewlyReleasedAnimesAsync(6);
            UpcomingAnime = await _animeManagement.GetUpcomingAnimeAsync();
            PopularAnimes = await _animeManagement.GetMostPopularAnimesAsync();
            TopWatchedAnimes = await _animeManagement.GetMostWatchedAnimesAsync();
            TopRatedAnime = await _animeManagement.GetTopRatedAnimesAsync(6);
        }
    }
}