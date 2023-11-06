using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_Shared.DomainModels;
using AniX_Shared.Enumerations;

namespace AniX_Shared.Interfaces
{
    public interface IAnimeManagement
    {
        Task<int> CreateAnimeAsync(Anime anime);
        Task<bool> UpdateAnimeAsync(Anime anime);
        Task<bool> DeleteAnimeAsync(int animeId);
        Task<Anime> GetAnimeByIdAsync(int animeId);
        Task<List<Anime>> GetAllAnimesAsync();
        Task<List<Anime>> GetAnimesByGenreAsync(string genreName);
        Task<List<Anime>> SearchAnimesAsync(string searchTerm);
        Task<List<Anime>> FilterAnimesAsync(string filter);
        Task<bool> DoesAnimeExistAsync(int animeId);
        Task<List<AnimeWithViewCount>> GetMostWatchedAnimesAsync();
        Task<List<AnimeWithPopularity>> GetMostPopularAnimesAsync();
        Task<(List<Anime> Animes, int TotalCount)> GetAnimesWithPaginationAsync(int page, int pageSize);
        Task<List<AnimeWithRatings>> GetAnimesWithRatingsAsync();
        Task<List<Anime>> GetRecommendedAnimesAsync(int userId);
        Task<bool> RecordAnimeViewAsync(AnimeViews animeView);
        Task<List<AnimeViews>> GetAnimeViewsAsync(int animeId);
    }
}
