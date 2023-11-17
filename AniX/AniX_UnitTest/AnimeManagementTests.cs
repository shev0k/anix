using Xunit;
using Moq;
using AniX_Shared.Interfaces;
using AniX_Shared.DomainModels;
using AniX_Shared.Extensions;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anix_Shared.DomainModels;
using Assert = Xunit.Assert;

namespace AniX_UnitTest
{
    public class AnimeManagementTests
    {
        private readonly Mock<IAnimeManagement> _mockAnimeManagement;
        private readonly Anime _testAnime;
        private readonly AnimeFilterModel _testFilter;

        public AnimeManagementTests()
        {
            _mockAnimeManagement = new Mock<IAnimeManagement>();

            _testAnime = new Anime
            {
                Id = 1,
                Name = "TestAnime",
                Description = "This is a test description for the anime.",
                ReleaseDate = DateTime.UtcNow,
                TrailerLink = "http://example.com/trailer",
                Country = "Japan",
                Season = "Spring",
                Episodes = 12,
                Studio = "TestStudio",
                Type = "TV",
                Status = "Airing",
                Premiered = "Spring 2023",
                Aired = DateTime.UtcNow,
                CoverImage = "http://example.com/cover.jpg",
                Thumbnail = "http://example.com/thumbnail.jpg",
                Language = "Japanese",
                Rating = "PG-13",
                Year = 2023,
                Reviews = new List<Review>(),
                Genres = new List<Genre>(),
                WatchedByUsers = new List<User>()
            };

            _testFilter = new AnimeFilterModel
            {
                GenreIds = new List<int> { 1, 2 },
                SearchQuery = "TestAnime",
                Countries = new List<string> { "Japan" },
                Premiered = new List<string> { "Spring 2023" },
                Years = new List<int> { 2023 },
                Types = new List<string> { "TV" },
                Statuses = new List<string> { "Airing" },
                Languages = new List<string> { "Japanese" },
                Ratings = new List<string> { "PG-13" },
                SortBy = SortCriteria.MostPopular
            };
        }
        [Fact]
        public async Task CreateAnimeAsync_Success()
        {
            _mockAnimeManagement.Setup(x => x.CreateAnimeAsync(It.IsAny<Anime>(), It.IsAny<List<int>>()))
                .ReturnsAsync(1);

            var result = await _mockAnimeManagement.Object.CreateAnimeAsync(_testAnime, new List<int> { 1, 2 });

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task CreateAnimeAsync_Failure()
        {
            _mockAnimeManagement.Setup(x => x.CreateAnimeAsync(It.IsAny<Anime>(), It.IsAny<List<int>>()))
                .ReturnsAsync(0);

            var result = await _mockAnimeManagement.Object.CreateAnimeAsync(_testAnime, new List<int> { 1, 2 });

            Assert.Equal(0, result);
        }
        [Fact]
        public async Task UpdateAnimeAsync_Success()
        {
            _mockAnimeManagement.Setup(x => x.UpdateAnimeAsync(It.IsAny<Anime>(), It.IsAny<List<int>>()))
                .ReturnsAsync(true);

            var result = await _mockAnimeManagement.Object.UpdateAnimeAsync(_testAnime, new List<int> { 3, 4 });

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAnimeAsync_Failure()
        {
            _mockAnimeManagement.Setup(x => x.UpdateAnimeAsync(It.IsAny<Anime>(), It.IsAny<List<int>>()))
                .ReturnsAsync(false);

            var result = await _mockAnimeManagement.Object.UpdateAnimeAsync(_testAnime, new List<int> { 3, 4 });

            Assert.False(result);
        }
        [Fact]
        public async Task DeleteAnimeAsync_Success()
        {
            _mockAnimeManagement.Setup(x => x.DeleteAnimeAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var result = await _mockAnimeManagement.Object.DeleteAnimeAsync(1);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAnimeAsync_Failure()
        {
            _mockAnimeManagement.Setup(x => x.DeleteAnimeAsync(It.IsAny<int>()))
                .ReturnsAsync(false);

            var result = await _mockAnimeManagement.Object.DeleteAnimeAsync(999);

            Assert.False(result);
        }
        [Fact]
        public async Task GetAnimeImageUrls_ReturnsUrls()
        {
            _mockAnimeManagement.Setup(x => x.GetAnimeImageUrls(It.IsAny<int>()))
                .ReturnsAsync(("http://example.com/cover.jpg", "http://example.com/thumbnail.jpg"));

            var (coverImageUrl, thumbnailUrl) = await _mockAnimeManagement.Object.GetAnimeImageUrls(1);

            Assert.NotNull(coverImageUrl);
            Assert.NotNull(thumbnailUrl);
        }
        [Fact]
        public async Task UpdateAnimeImages_Success()
        {
            _mockAnimeManagement.Setup(x => x.UpdateAnimeImages(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await _mockAnimeManagement.Object.UpdateAnimeImages(1, "http://example.com/newcover.jpg", "http://example.com/newthumbnail.jpg");

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAnimeImages_Failure()
        {
            _mockAnimeManagement.Setup(x => x.UpdateAnimeImages(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await _mockAnimeManagement.Object.UpdateAnimeImages(999, "http://example.com/newcover.jpg", "http://example.com/newthumbnail.jpg");

            Assert.False(result);
        }
        [Fact]
        public async Task GetAnimeDetailAsync_ReturnsDetail()
        {
            _mockAnimeManagement.Setup(x => x.GetAnimeDetailAsync(It.IsAny<int>(), It.IsAny<int?>()))
                .ReturnsAsync(new AnimeDetailModel { Anime = _testAnime });

            var result = await _mockAnimeManagement.Object.GetAnimeDetailAsync(1);

            Assert.NotNull(result);
            Assert.Equal("TestAnime", result.Anime.Name);
        }
        [Fact]
        public async Task GetRecommendedAnimesAsync_ReturnsAnimes()
        {
            _mockAnimeManagement.Setup(x => x.GetRecommendedAnimesAsync(It.IsAny<int?>(), It.IsAny<int?>()))
                .ReturnsAsync(new List<AnimeWithRatings> { new AnimeWithRatings { Name = "Anime1" } });

            var result = await _mockAnimeManagement.Object.GetRecommendedAnimesAsync(1, null);

            Assert.Single(result);
            Assert.Equal("Anime1", result[0].Name);
        }
        [Fact]
        public async Task GetUpcomingAnimeAsync_ReturnsAnime()
        {
            _mockAnimeManagement.Setup(x => x.GetUpcomingAnimeAsync())
                .ReturnsAsync(new AnimeWithRatings { Name = "UpcomingAnime" });

            var result = await _mockAnimeManagement.Object.GetUpcomingAnimeAsync();

            Assert.NotNull(result);
            Assert.Equal("UpcomingAnime", result.Name);
        }
        [Fact]
        public async Task GetFilteredAnimesAsync_ReturnsFilteredAnimes()
        {
            _mockAnimeManagement.Setup(x => x.GetFilteredAnimesAsync(It.IsAny<AnimeFilterModel>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((new List<AnimeWithRatings> { new AnimeWithRatings { Name = "FilteredAnime" } }, 1));

            var (animes, totalCount) = await _mockAnimeManagement.Object.GetFilteredAnimesAsync(_testFilter, 1, 10);

            Assert.Single(animes);
            Assert.Equal(1, totalCount);
            Assert.Equal("FilteredAnime", animes[0].Name);
        }
        [Fact]
        public async Task GetTopRatedAnimesAsync_ReturnsAnimes()
        {
            _mockAnimeManagement.Setup(x => x.GetTopRatedAnimesAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<AnimeWithRatings> { new AnimeWithRatings { Name = "TopAnime" } });

            var result = await _mockAnimeManagement.Object.GetTopRatedAnimesAsync(5);

            Assert.Single(result);
            Assert.Equal("TopAnime", result[0].Name);
        }
        [Fact]
        public async Task GetNewlyReleasedAnimesAsync_ReturnsAnimes()
        {
            _mockAnimeManagement.Setup(x => x.GetNewlyReleasedAnimesAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<AnimeWithRatings> { new AnimeWithRatings { Name = "NewAnime" } });

            var result = await _mockAnimeManagement.Object.GetNewlyReleasedAnimesAsync(5);

            Assert.Single(result);
            Assert.Equal("NewAnime", result[0].Name);
        }
        [Fact]
        public async Task GetRecentlyUpdatedAnimesAsync_ReturnsAnimes()
        {
            _mockAnimeManagement.Setup(x => x.GetRecentlyUpdatedAnimesAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<AnimeWithRatings> { new AnimeWithRatings { Name = "UpdatedAnime" } });

            var result = await _mockAnimeManagement.Object.GetRecentlyUpdatedAnimesAsync(5);

            Assert.Single(result);
            Assert.Equal("UpdatedAnime", result[0].Name);
        }
        [Fact]
        public async Task GetRandomAnimesAsync_ReturnsAnimes()
        {
            _mockAnimeManagement.Setup(x => x.GetRandomAnimesAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<AnimeWithRatings> { new AnimeWithRatings { Name = "RandomAnime" } });

            var result = await _mockAnimeManagement.Object.GetRandomAnimesAsync(5);

            Assert.Single(result);
            Assert.Equal("RandomAnime", result[0].Name);
        }
        [Fact]
        public async Task GetRandomAnimeAsync_ReturnsAnime()
        {
            _mockAnimeManagement.Setup(x => x.GetRandomAnimeAsync())
                .ReturnsAsync(new AnimeWithRatings { Name = "RandomSingleAnime" });

            var result = await _mockAnimeManagement.Object.GetRandomAnimeAsync();

            Assert.NotNull(result);
            Assert.Equal("RandomSingleAnime", result.Name);
        }
        [Fact]
        public async Task GetSearchSuggestionsAsync_ReturnsSuggestions()
        {
            _mockAnimeManagement.Setup(x => x.GetSearchSuggestionsAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<string> { "Suggestion1", "Suggestion2" });

            var result = await _mockAnimeManagement.Object.GetSearchSuggestionsAsync("genre", "action");

            Assert.Equal(2, result.Count);
            Assert.Contains("Suggestion1", result);
        }
        [Fact]
        public async Task FetchFilteredAndSearchedAnimesAsync_ReturnsAnimes()
        {
            _mockAnimeManagement.Setup(x => x.FetchFilteredAndSearchedAnimesAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<Anime> { new Anime { Name = "FilteredSearchAnime" } });

            var result = await _mockAnimeManagement.Object.FetchFilteredAndSearchedAnimesAsync("filter", "searchTerm");

            Assert.Single(result);
            Assert.Equal("FilteredSearchAnime", result[0].Name);
        }
        [Fact]
        public async Task DoesAnimeNameExistAsync_True()
        {
            _mockAnimeManagement.Setup(x => x.DoesAnimeNameExistAsync("ExistingAnime"))
                .ReturnsAsync(true);

            var exists = await _mockAnimeManagement.Object.DoesAnimeNameExistAsync("ExistingAnime");

            Assert.True(exists);
        }

        [Fact]
        public async Task DoesAnimeNameExistAsync_False()
        {
            _mockAnimeManagement.Setup(x => x.DoesAnimeNameExistAsync("NewAnime"))
                .ReturnsAsync(false);

            var exists = await _mockAnimeManagement.Object.DoesAnimeNameExistAsync("NewAnime");

            Assert.False(exists);
        }
        [Fact]
        public async Task GetMostWatchedAnimesAsync_ReturnsAnimes()
        {
            _mockAnimeManagement.Setup(x => x.GetMostWatchedAnimesAsync())
                .ReturnsAsync(new List<AnimeWithViewCount> { new AnimeWithViewCount { Name = "MostWatchedAnime", ViewCount = 100 } });

            var result = await _mockAnimeManagement.Object.GetMostWatchedAnimesAsync();

            Assert.Single(result);
            Assert.Equal("MostWatchedAnime", result[0].Name);
        }
        [Fact]
        public async Task GetMostPopularAnimesAsync_ReturnsAnimes()
        {
            _mockAnimeManagement.Setup(x => x.GetMostPopularAnimesAsync())
                .ReturnsAsync(new List<AnimeWithPopularity> { new AnimeWithPopularity { Name = "PopularAnime", WatchlistCount = 50 } });

            var result = await _mockAnimeManagement.Object.GetMostPopularAnimesAsync();

            Assert.Single(result);
            Assert.Equal("PopularAnime", result[0].Name);
        }
        [Fact]
        public async Task RecordAnimeViewAsync_Success()
        {
            _mockAnimeManagement.Setup(x => x.RecordAnimeViewAsync(It.IsAny<AnimeViews>()))
                .ReturnsAsync(true);

            var result = await _mockAnimeManagement.Object.RecordAnimeViewAsync(new AnimeViews { AnimeId = 1, UserId = 1 });

            Assert.True(result);
        }

        [Fact]
        public async Task RecordAnimeViewAsync_Failure()
        {
            _mockAnimeManagement.Setup(x => x.RecordAnimeViewAsync(It.IsAny<AnimeViews>()))
                .ReturnsAsync(false);

            var result = await _mockAnimeManagement.Object.RecordAnimeViewAsync(new AnimeViews { AnimeId = 999, UserId = 999 });

            Assert.False(result);
        }
        [Fact]
        public async Task GetAnimeViewsAsync_ReturnsViews()
        {
            _mockAnimeManagement.Setup(x => x.GetAnimeViewsAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<AnimeViews> { new AnimeViews { AnimeId = 1, UserId = 1, ViewDate = DateTime.Now } });

            var result = await _mockAnimeManagement.Object.GetAnimeViewsAsync(1);

            Assert.Single(result);
            Assert.Equal(1, result[0].AnimeId);
        }
        [Fact]
        public async Task GetAllGenresAsync_ReturnsGenres()
        {
            _mockAnimeManagement.Setup(x => x.GetAllGenresAsync())
                .ReturnsAsync(new List<Genre> { new Genre { Id = 1, Name = "Action" } });

            var result = await _mockAnimeManagement.Object.GetAllGenresAsync();

            Assert.Single(result);
            Assert.Equal("Action", result[0].Name);
        }

    }
}