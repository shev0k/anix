using AniX_Shared.DomainModels;
using AniX_Shared.Interfaces;
using AniX_Utility;
using System;
using System.Threading.Tasks;

namespace AniX_FormsLogic
{
    public class AnimeAddEditFormLogic
    {
        private readonly ApplicationModel _appModel;
        private readonly IAzureBlobService _azureBlobService;

        public AnimeAddEditFormLogic(ApplicationModel appModel, IAzureBlobService azureBlobService)
        {
            _appModel = appModel;
            _azureBlobService = azureBlobService;
        }

        public async Task<OperationResult> AddNewAnimeAsync(Anime anime, List<int> genreIds, Stream coverImageStream, Stream thumbnailStream)
        {
            try
            {
                int animeId = await _appModel.AnimeController.CreateAnimeAsync(anime, genreIds);
                if (animeId <= 0)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Failed to add anime."
                    };
                }

                anime.Id = animeId;

                string coverImageUrl = anime.CoverImage;
                string thumbnailUrl = anime.Thumbnail;

                if (coverImageStream != null)
                {
                    coverImageUrl = await _azureBlobService.UploadAnimeImageAsync(
                                       coverImageStream, $"cover_{anime.Id}_" + Guid.NewGuid().ToString(), "image/png");
                }

                if (thumbnailStream != null)
                {
                    thumbnailUrl = await _azureBlobService.UploadAnimeImageAsync(
                                       thumbnailStream, $"thumbnail_{anime.Id}_" + Guid.NewGuid().ToString(), "image/png");
                }

                bool updateImagesResult = await _appModel.AnimeController.UpdateAnimeImages(anime.Id, coverImageUrl, thumbnailUrl);
                if (!updateImagesResult)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Failed to update anime images."
                    };
                }

                return new OperationResult
                {
                    Success = true,
                    Message = "Anime added successfully."
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


        public async Task<OperationResult> UpdateExistingAnimeAsync(Anime anime, List<int> newGenreIds, Stream coverImageStream, Stream thumbnailStream)
        {
            try
            {
                var imageUrls = await _appModel.AnimeController.GetAnimeImageUrls(anime.Id);
                string coverImageUrl = imageUrls.CoverImageUrl;
                string thumbnailUrl = imageUrls.ThumbnailUrl;

                bool IsValidUri(string uri) => Uri.TryCreate(uri, UriKind.Absolute, out Uri _);

                if (coverImageStream != null)
                {
                    coverImageUrl = await _azureBlobService.UploadAnimeImageAsync(
                                        coverImageStream, $"cover_{anime.Id}_" + Guid.NewGuid().ToString(), "image/png");

                    if (IsValidUri(imageUrls.CoverImageUrl) && !string.IsNullOrEmpty(imageUrls.CoverImageUrl) && coverImageUrl != imageUrls.CoverImageUrl)
                    {
                        await _azureBlobService.DeleteImageAsync(imageUrls.CoverImageUrl);
                    }
                }

                if (thumbnailStream != null)
                {
                    thumbnailUrl = await _azureBlobService.UploadAnimeImageAsync(
                                       thumbnailStream, $"thumbnail_{anime.Id}_" + Guid.NewGuid().ToString(), "image/png");

                    if (IsValidUri(imageUrls.ThumbnailUrl) && !string.IsNullOrEmpty(imageUrls.ThumbnailUrl) && thumbnailUrl != imageUrls.ThumbnailUrl)
                    {
                        await _azureBlobService.DeleteImageAsync(imageUrls.ThumbnailUrl);
                    }
                }

                anime.CoverImage = coverImageUrl;
                anime.Thumbnail = thumbnailUrl;

                bool success = await _appModel.AnimeController.UpdateAnimeAsync(anime, newGenreIds);
                if (!success)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Failed to update anime details."
                    };
                }

                success = await _appModel.AnimeController.UpdateAnimeImages(anime.Id, coverImageUrl, thumbnailUrl);
                if (!success)
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Failed to update anime images."
                    };
                }

                return new OperationResult
                {
                    Success = true,
                    Message = "Anime updated successfully."
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

