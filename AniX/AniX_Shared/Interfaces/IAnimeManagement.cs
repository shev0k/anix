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
        Task<AnimeDetailModel> GetAnimeDetailAsync(int animeId, int? currentUserId = null);
        Task<List<AnimeWithRatings>> GetRecommendedAnimesAsync(int? userId, int? currentAnimeId);
        Task<AnimeWithRatings> GetUpcomingAnimeAsync();
        Task<(List<AnimeWithRatings> Animes, int TotalCount)> GetFilteredAnimesAsync(AnimeFilterModel filter, int pageNumber, int pageSize);
        Task<List<AnimeWithRatings>> GetTopRatedAnimesAsync(int count);
        Task<List<AnimeWithRatings>> GetNewlyReleasedAnimesAsync(int count);
        Task<List<AnimeWithRatings>> GetRecentlyUpdatedAnimesAsync(int count);
        Task<List<AnimeWithRatings>> GetRandomAnimesAsync(int count);
        Task<AnimeWithRatings> GetRandomAnimeAsync();
        Task<List<string>> GetSearchSuggestionsAsync(string searchType, string searchTerm);
        Task<List<Anime>> FetchFilteredAndSearchedAnimesAsync(string filter, string searchTerm);
        Task<bool> DoesAnimeNameExistAsync(string animeName);
        Task<List<AnimeWithViewCount>> GetMostWatchedAnimesAsync();
        Task<List<AnimeWithPopularity>> GetMostPopularAnimesAsync();
        Task<bool> RecordAnimeViewAsync(AnimeViews animeView);
        Task<List<AnimeViews>> GetAnimeViewsAsync(int animeId);
        Task<List<Genre>> GetAllGenresAsync();
    }
}
