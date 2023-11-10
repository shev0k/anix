using AniX_Shared.DomainModels;
using AniX_Utility;
using System;
using System.Threading.Tasks;

namespace AniX_FormsLogic
{
    public class AnimeAddEditFormLogic
    {
        private readonly ApplicationModel _appModel;

        public AnimeAddEditFormLogic(ApplicationModel appModel)
        {
            _appModel = appModel;
        }

        public async Task<OperationResult> AddNewAnimeAsync(Anime anime, List<int> genreIds)
        {
            try
            {
                int animeId = await _appModel.AnimeController.CreateAnimeAsync(anime, genreIds);
                return new OperationResult
                {
                    Success = animeId > 0,
                    Message = animeId > 0 ? "Anime added successfully." : "Failed to add anime."
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"An error occurred while adding the anime: {ex.Message}"
                };
            }
        }

        public async Task<OperationResult> UpdateExistingAnimeAsync(Anime anime, List<int> newGenreIds)
        {
            try
            {
                bool success = await _appModel.AnimeController.UpdateAnimeAsync(anime, newGenreIds);
                return new OperationResult
                {
                    Success = success,
                    Message = success ? "Anime updated successfully." : "Failed to update anime."
                };
            }
            catch (Exception ex)
            {
                return new OperationResult
                {
                    Success = false,
                    Message = $"An error occurred while updating the anime: {ex.Message}"
                };
            }
        }

        public async Task<(bool IsValid, string Message)> ValidateAnimeFormAsync(
                string name,
                string country,
                string language,
                string season,
                string episodes,
                string studio,
                object rating,
                object type,
                object status,
                object premiered,
                string trailerLink,
                string banner,
                string thumbnail,
                string description,
                int genreCount,
                bool isEditMode)
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Anime name is required.");

            if (!isEditMode)
            {
                bool nameExists = await _appModel.AnimeController.DoesAnimeNameExistAsync(name);
                if (nameExists)
                    return (false, "Anime with this name already exists.");
            }

            if (string.IsNullOrWhiteSpace(country))
                return (false, "Country of origin is required.");

            if (string.IsNullOrWhiteSpace(language))
                return (false, "Language is required.");

            if (string.IsNullOrWhiteSpace(season))
                return (false, "Season is required.");

            if (!int.TryParse(episodes, out _))
                return (false, "Episode count must be a number.");

            if (string.IsNullOrWhiteSpace(studio))
                return (false, "Studio is required.");

            if (rating == null)
                return (false, "Rating selection is required.");

            if (type == null)
                return (false, "Type selection is required.");

            if (status == null)
                return (false, "Status selection is required.");

            if (premiered == null)
                return (false, "Premiered selection is required.");

            if (string.IsNullOrWhiteSpace(trailerLink))
                return (false, "Trailer link is required.");

            if (string.IsNullOrWhiteSpace(banner))
                return (false, "Banner image is required.");

            if (string.IsNullOrWhiteSpace(thumbnail))
                return (false, "Thumbnail image is required.");

            if (string.IsNullOrWhiteSpace(description))
                return (false, "Description is required.");

            if (genreCount == 0)
                return (false, "At least one genre must be selected.");

            return (true, "Validation succeeded");
        }
    }
}

