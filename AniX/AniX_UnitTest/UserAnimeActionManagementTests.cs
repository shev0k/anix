using Xunit;
using Moq;
using AniX_Shared.Interfaces;
using AniX_Shared.DomainModels;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AniX_Utility;
using Assert = Xunit.Assert;

namespace AniX_UnitTest
{
    public class UserAnimeActionManagementTests
    {
        private readonly Mock<IUserAnimeActionManagement> _mockUserAnimeActionManagement;
        private readonly UserAnimeAction _testUserAnimeAction;

        public UserAnimeActionManagementTests()
        {
            _mockUserAnimeActionManagement = new Mock<IUserAnimeActionManagement>();
            _testUserAnimeAction = new WatchLater
            {
                UserId = 1,
                Anime = new Anime { Id = 1}
            };
        }

        [Fact]
        public async Task AddUserAnimeActionAsync_Success()
        {
            _mockUserAnimeActionManagement.Setup(x => x.AddUserAnimeActionAsync(It.IsAny<UserAnimeAction>()))
                .ReturnsAsync(new OperationResult { Success = true });

            var result = await _mockUserAnimeActionManagement.Object.AddUserAnimeActionAsync(_testUserAnimeAction);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task AddUserAnimeActionAsync_Failure()
        {
            _mockUserAnimeActionManagement.Setup(x => x.AddUserAnimeActionAsync(It.IsAny<UserAnimeAction>()))
                .ReturnsAsync(new OperationResult { Success = false });

            var result = await _mockUserAnimeActionManagement.Object.AddUserAnimeActionAsync(_testUserAnimeAction);

            Assert.False(result.Success);
        }
        [Fact]
        public async Task RemoveUserAnimeActionAsync_Success()
        {
            _mockUserAnimeActionManagement.Setup(x => x.RemoveUserAnimeActionAsync(It.IsAny<UserAnimeAction>()))
                .ReturnsAsync(new OperationResult { Success = true });

            var result = await _mockUserAnimeActionManagement.Object.RemoveUserAnimeActionAsync(_testUserAnimeAction);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task RemoveUserAnimeActionAsync_Failure()
        {
            _mockUserAnimeActionManagement.Setup(x => x.RemoveUserAnimeActionAsync(It.IsAny<UserAnimeAction>()))
                .ReturnsAsync(new OperationResult { Success = false });

            var result = await _mockUserAnimeActionManagement.Object.RemoveUserAnimeActionAsync(_testUserAnimeAction);

            Assert.False(result.Success);
        }
        [Fact]
        public async Task GetUserWatchlistAsync_ReturnsWatchlist()
        {
            _mockUserAnimeActionManagement.Setup(x => x.GetUserWatchlistAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<WatchLater> { new WatchLater { UserId = 1, Anime = new Anime() } });

            var result = await _mockUserAnimeActionManagement.Object.GetUserWatchlistAsync(1);

            Assert.Single(result);
        }

        [Fact]
        public async Task GetUserWatchlistAsync_ReturnsEmpty()
        {
            _mockUserAnimeActionManagement.Setup(x => x.GetUserWatchlistAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<WatchLater>());

            var result = await _mockUserAnimeActionManagement.Object.GetUserWatchlistAsync(999);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetUserPlaylistAsync_ReturnsPlaylist()
        {
            _mockUserAnimeActionManagement.Setup(x => x.GetUserPlaylistAsync(It.IsAny<int>()))
                                          .ReturnsAsync(new List<PlaylistItem> { new PlaylistItem { UserId = 1, Anime = new Anime() } });

            var result = await _mockUserAnimeActionManagement.Object.GetUserPlaylistAsync(1);

            Assert.Single(result);
        }

        [Fact]
        public async Task GetUserPlaylistAsync_ReturnsEmpty()
        {
            _mockUserAnimeActionManagement.Setup(x => x.GetUserPlaylistAsync(It.IsAny<int>()))
                                          .ReturnsAsync(new List<PlaylistItem>());

            var result = await _mockUserAnimeActionManagement.Object.GetUserPlaylistAsync(999);

            Assert.Empty(result);
        }
        [Fact]
        public async Task IsAnimeInUserWatchlist_True()
        {
            _mockUserAnimeActionManagement.Setup(x => x.IsAnimeInUserWatchlist(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            var isInWatchlist = await _mockUserAnimeActionManagement.Object.IsAnimeInUserWatchlist(1, 1);

            Assert.True(isInWatchlist);
        }

        [Fact]
        public async Task IsAnimeInUserWatchlist_False()
        {
            _mockUserAnimeActionManagement.Setup(x => x.IsAnimeInUserWatchlist(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            var isInWatchlist = await _mockUserAnimeActionManagement.Object.IsAnimeInUserWatchlist(1, 999);

            Assert.False(isInWatchlist);
        }
        [Fact]
        public async Task IsAnimeInUserPlaylist_True()
        {
            _mockUserAnimeActionManagement.Setup(x => x.IsAnimeInUserPlaylist(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            var isInPlaylist = await _mockUserAnimeActionManagement.Object.IsAnimeInUserPlaylist(1, 1);

            Assert.True(isInPlaylist);
        }

        [Fact]
        public async Task IsAnimeInUserPlaylist_False()
        {
            _mockUserAnimeActionManagement.Setup(x => x.IsAnimeInUserPlaylist(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(false);

            var isInPlaylist = await _mockUserAnimeActionManagement.Object.IsAnimeInUserPlaylist(1, 999);

            Assert.False(isInPlaylist);
        }

    }
}