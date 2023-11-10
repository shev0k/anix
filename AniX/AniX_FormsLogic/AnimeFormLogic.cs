using Anix_Shared.DomainModels;
using AniX_Shared.DomainModels;
using AniX_Utility;
using System.Xml.Linq;

namespace AniX_FormsLogic
{
    public class AnimeFormLogic
    {
        private ApplicationModel _appModel;

        public AnimeFormLogic(ApplicationModel appModel)
        {
            _appModel = appModel;
        }

        public async Task<List<Anime>> RefreshAnimesAsync()
        {
            return await _appModel.AnimeController.FetchFilteredAndSearchedAnimesAsync("All Anime", null);
        }

        public async Task<List<Anime>> UpdateAnimeListAsync(string selectedFilter, string searchTerm)
        {
            return await _appModel.AnimeController.FetchFilteredAndSearchedAnimesAsync(selectedFilter, searchTerm);
        }

        public async Task<List<string>> GetSearchSuggestionsAsync(string searchType, string searchTerm)
        {
            return await _appModel.AnimeController.GetSearchSuggestionsAsync(searchType, searchTerm);
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _appModel.AnimeController.GetAllGenresAsync();
        }

        public async Task<OperationResult> DeleteAnimeAsync(int animeId)
        {
            bool success = await _appModel.AnimeController.DeleteAnimeAsync(animeId);
            return new OperationResult
            {
                Success = success,
                Message = success ? "Anime deleted successfully." : "Failed to delete anime."
            };
        }

        public string ShowAnimeDetails(Anime anime)
        {
            var genres = anime.Genres?.Select(g => g.Name) ?? new List<string>();

            return
                   $"Type: {anime.Type}\n" +
                   $"Episodes: {anime.Episodes ?? 0}\n" +
                   $"Status: {anime.Status}\n" +
                   $"Premiered: {anime.Premiered} {anime.Year}\n" +
                   $"Studio: {anime.Studio}\n" +
                   $"Genres: {string.Join(", ", genres)}\n";
        }

        public Anime GetSelectedAnimeFromDataGridView(List<Tuple<Anime, object>> originalAnimes, int rowIndex)
        {
            return originalAnimes[rowIndex].Item1;
        }

        public List<Tuple<Anime, object>> TransformAnimesForDataGridView(List<Anime> animes)
        {
            return animes.Select(anime => new Tuple<Anime, object>(
                anime,
                new
                {
                    anime.Id,
                    anime.Name,
                    anime.Country,
                    anime.Language,
                    anime.Year,
                    anime.Studio,
                    anime.Rating,
                    anime.Type,
                    anime.Status,
                    anime.Premiered,
                    Genre = string.Join(", ", anime.Genres.Select(g => g.Name).Distinct()) // Ensure unique genre names
                }
            )).ToList();
        }


    }
}