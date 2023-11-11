using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniX_BusinessLogic;
using Anix_Shared.DomainModels;
using AniX_Shared.DomainModels;
using AniX_Shared.Extensions;
using AniX_Shared.Interfaces;
using AniX_Utility;

namespace AniX_Controllers
{
    public class AnimeController
    {
        private readonly IAnimeManagement _animeManagement;
        private readonly IExceptionHandlingService _exceptionHandlingService;
        private readonly IErrorLoggingService _errorLoggingService;
        private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public AnimeController(
            IAnimeManagement animeManagement,
            IExceptionHandlingService exceptionHandlingService,
            IErrorLoggingService errorLoggingService)
        {
            _animeManagement = animeManagement;
            _exceptionHandlingService = exceptionHandlingService;
            _errorLoggingService = errorLoggingService;
        }


        public async Task<int> CreateAnimeAsync(Anime anime, List<int> genreIds)
        {
            return await ExecuteWithExceptionHandlingAsync(() =>
            {
                ValidateAnime(anime);
                return _animeManagement.CreateAnimeAsync(anime, genreIds);
            });
        }

        public async Task<(string CoverImageUrl, string ThumbnailUrl)> GetAnimeImageUrls(int animeId)
        {
            return await ExecuteWithExceptionHandlingAsync(() =>
            {
                return _animeManagement.GetAnimeImageUrls(animeId);
            });
        }

        public async Task<bool> UpdateAnimeImages(int animeId, string coverImageUrl, string thumbnailUrl)
        {
            return await ExecuteWithExceptionHandlingAsync(async () =>
            {
                return await _animeManagement.UpdateAnimeImages(animeId, coverImageUrl, thumbnailUrl);
            });
        }

        public async Task<bool> UpdateAnimeAsync(Anime anime, List<int> newGenreIds)
        {
            return await ExecuteWithExceptionHandlingAsync(() =>
            {
                ValidateAnime(anime);
                return _animeManagement.UpdateAnimeAsync(anime, newGenreIds);
            });
        }

        public async Task<bool> DeleteAnimeAsync(int animeId)
        {
            return await ExecuteWithExceptionHandlingAsync(() =>
            {
                if (animeId <= 0)
                {
                    throw new ArgumentException("Anime ID must be positive.", nameof(animeId));
                }
                return _animeManagement.DeleteAnimeAsync(animeId);
            });
        }

        public async Task<List<string>> GetSearchSuggestionsAsync(string searchType, string searchTerm)
        {
            await semaphore.WaitAsync();
            try
            {
                return await _animeManagement.GetSearchSuggestionsAsync(searchType, searchTerm);
            }
            catch (Exception e)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(e);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(e, LogSeverity.Critical);
                }
                throw;
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            return await _animeManagement.GetAllGenresAsync();
        }

        public async Task<List<Anime>> FetchFilteredAndSearchedAnimesAsync(string filter, string searchTerm)
        {
            await semaphore.WaitAsync();
            try
            {
                return await _animeManagement.FetchFilteredAndSearchedAnimesAsync(filter, searchTerm);
            }
            catch (Exception e)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(e);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(e, LogSeverity.Critical);
                }
                throw;
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task<bool> DoesAnimeNameExistAsync(string animeName)
        {
            return await ExecuteWithExceptionHandlingAsync(() =>
            {
                if (string.IsNullOrWhiteSpace(animeName))
                {
                    throw new ArgumentException("Anime name cannot be null or whitespace.", nameof(animeName));
                }
                return _animeManagement.DoesAnimeNameExistAsync(animeName);
            });
        }

        private void ValidateAnime(Anime anime)
        {
            var validationErrors = new List<string>();

            if (string.IsNullOrWhiteSpace(anime.Name))
            {
                validationErrors.Add("Anime name cannot be empty.");
            }

            if (anime.Episodes.HasValue && anime.Episodes < 1)
            {
                validationErrors.Add("Number of episodes must be at least 1.");
            }

            //if (!string.IsNullOrWhiteSpace(anime.TrailerLink) && !Uri.IsWellFormedUriString(anime.TrailerLink, UriKind.Absolute))
            //{
            //    validationErrors.Add("Trailer link is not a valid URL.");
            //}

            if (validationErrors.Any())
            {
                throw new ValidationException($"Anime validation failed: {string.Join(" ", validationErrors)}");
            }
        }

        private async Task<T> ExecuteWithExceptionHandlingAsync<T>(Func<Task<T>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception e)
            {
                bool handled = await _exceptionHandlingService.HandleExceptionAsync(e);
                if (!handled)
                {
                    await _errorLoggingService.LogErrorAsync(e, LogSeverity.Critical);
                }
                throw;
            }
        }

        public class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }
        }
    }
}
