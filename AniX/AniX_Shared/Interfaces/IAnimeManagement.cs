using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_Shared.DomainModels; 
using AniX_Shared.Extensions;

namespace AniX_Shared.Interfaces
{
    public interface IAnimeManagement
    {
        Task<int> CreateAnimeAsync(Anime anime, List<int> genreIds);
        Task<bool> UpdateAnimeAsync(Anime anime, List<int> newGenreIds);
        Task<bool> DeleteAnimeAsync(int animeId);
        Task<(string coverImageUrl, string thumbnailUrl)> GetAnimeImageUrls(int animeId);
        Task<bool> UpdateAnimeImages(int animeId, string coverImageUrl, string thumbnailUrl);
        Task<Anime> GetAnimeByIdAsync(int animeId);
        Task<List<string>> GetSearchSuggestionsAsync(string searchType, string searchTerm);
        Task<List<Anime>> GetAllAnimesAsync();
        Task<List<Anime>> GetAnimesByGenreAsync(string genreName);
        Task<List<Anime>> FetchFilteredAndSearchedAnimesAsync(string filter, string searchTerm);
        Task<bool> DoesAnimeExistAsync(int animeId);
        Task<bool> DoesAnimeNameExistAsync(string animeName);
        Task<List<AnimeWithViewCount>> GetMostWatchedAnimesAsync();
        Task<List<AnimeWithPopularity>> GetMostPopularAnimesAsync();
        Task<(List<Anime> Animes, int TotalCount)> GetAnimesWithPaginationAsync(int page, int pageSize);
        Task<List<AnimeWithRatings>> GetAnimesWithRatingsAsync();
        Task<List<Anime>> GetRecommendedAnimesAsync(int userId);
        Task<bool> RecordAnimeViewAsync(AnimeViews animeView);
        Task<List<AnimeViews>> GetAnimeViewsAsync(int animeId);
        Task<List<Genre>> GetAllGenresAsync();
    }
}
