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
    public class ReviewManagementTests
    {
        private readonly Mock<IReviewManagement> _mockReviewManagement;
        private readonly Review _testReview;

        public ReviewManagementTests()
        {
            _mockReviewManagement = new Mock<IReviewManagement>();
            _testReview = new Review
            {
                Id = 1,
                UserId = 1,
                AnimeId = 1,
                Text = "Great anime!",
                Rating = 4.5,
                IsApproved = false,
                UserName = "TestUser",
                AnimeName = "TestAnime"
            };
        }

        [Fact]
        public async Task CreateReviewAsync_Success()
        {
            _mockReviewManagement.Setup(x => x.CreateReviewAsync(It.IsAny<Review>()))
                .ReturnsAsync(new OperationResult { Success = true });

            var result = await _mockReviewManagement.Object.CreateReviewAsync(_testReview);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task CreateReviewAsync_Failure()
        {
            _mockReviewManagement.Setup(x => x.CreateReviewAsync(It.IsAny<Review>()))
                .ReturnsAsync(new OperationResult { Success = false });

            var result = await _mockReviewManagement.Object.CreateReviewAsync(_testReview);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateReviewRatingAsync_Success()
        {
            _mockReviewManagement.Setup(x => x.UpdateReviewRatingAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(new OperationResult { Success = true });

            var result = await _mockReviewManagement.Object.UpdateReviewRatingAsync(1, 1, 5.0);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateReviewRatingAsync_Failure()
        {
            _mockReviewManagement.Setup(x => x.UpdateReviewRatingAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(new OperationResult { Success = false });

            var result = await _mockReviewManagement.Object.UpdateReviewRatingAsync(1, 1, 5.0);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task DeleteReviewAsync_Success()
        {
            _mockReviewManagement.Setup(x => x.DeleteReviewAsync(It.IsAny<int>()))
                .ReturnsAsync(new OperationResult { Success = true });

            var result = await _mockReviewManagement.Object.DeleteReviewAsync(1);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task DeleteReviewAsync_Failure()
        {
            _mockReviewManagement.Setup(x => x.DeleteReviewAsync(It.IsAny<int>()))
                .ReturnsAsync(new OperationResult { Success = false });

            var result = await _mockReviewManagement.Object.DeleteReviewAsync(999);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task ApproveReviewAsync_Success()
        {
            _mockReviewManagement.Setup(x => x.ApproveReviewAsync(It.IsAny<int>()))
                .ReturnsAsync(new OperationResult { Success = true });

            var result = await _mockReviewManagement.Object.ApproveReviewAsync(1);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task ApproveReviewAsync_Failure()
        {
            _mockReviewManagement.Setup(x => x.ApproveReviewAsync(It.IsAny<int>()))
                .ReturnsAsync(new OperationResult { Success = false });

            var result = await _mockReviewManagement.Object.ApproveReviewAsync(999);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task GetReviewsByUserIdAndAnimeIdAsync_ReturnsReviews()
        {
            _mockReviewManagement.Setup(x => x.GetReviewsByUserIdAndAnimeIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Review> { _testReview });

            var result = await _mockReviewManagement.Object.GetReviewsByUserIdAndAnimeIdAsync(1, 1);

            Assert.Single(result);
        }

        [Fact]
        public async Task GetReviewsByUserIdAndAnimeIdAsync_ReturnsEmpty()
        {
            _mockReviewManagement.Setup(x => x.GetReviewsByUserIdAndAnimeIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new List<Review>());

            var result = await _mockReviewManagement.Object.GetReviewsByUserIdAndAnimeIdAsync(1, 999);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetReviewByIdAsync_Success()
        {
            _mockReviewManagement.Setup(x => x.GetReviewByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_testReview);

            var result = await _mockReviewManagement.Object.GetReviewByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetReviewByIdAsync_Failure()
        {
            _mockReviewManagement.Setup(x => x.GetReviewByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Review)null);

            var result = await _mockReviewManagement.Object.GetReviewByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetPendingReviewsAsync_ReturnsPendingReviews()
        {
            _mockReviewManagement.Setup(x => x.GetPendingReviewsAsync())
                .ReturnsAsync(new List<Review> { _testReview });

            var result = await _mockReviewManagement.Object.GetPendingReviewsAsync();

            Assert.Single(result);
            Assert.False(result[0].IsApproved);
        }

        [Fact]
        public async Task GetPendingReviewsAsync_ReturnsEmpty()
        {
            _mockReviewManagement.Setup(x => x.GetPendingReviewsAsync())
                .ReturnsAsync(new List<Review>());

            var result = await _mockReviewManagement.Object.GetPendingReviewsAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetApprovedReviewsByAnimeIdAsync_ReturnsApprovedReviews()
        {
            _testReview.IsApproved = true;
            _mockReviewManagement.Setup(x => x.GetApprovedReviewsByAnimeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<Review> { _testReview });

            var result = await _mockReviewManagement.Object.GetApprovedReviewsByAnimeIdAsync(1);

            Assert.Single(result);
            Assert.True(result[0].IsApproved);
        }

        [Fact]
        public async Task GetApprovedReviewsByAnimeIdAsync_ReturnsEmpty()
        {
            _mockReviewManagement.Setup(x => x.GetApprovedReviewsByAnimeIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new List<Review>());

            var result = await _mockReviewManagement.Object.GetApprovedReviewsByAnimeIdAsync(999);

            Assert.Empty(result);
        }

        [Fact]
        public async Task FetchFilteredReviewsAsync_ReturnsFilteredReviews()
        {
            _mockReviewManagement.Setup(x => x.FetchFilteredReviewsAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Review> { _testReview });

            var result = await _mockReviewManagement.Object.FetchFilteredReviewsAsync("filter");

            Assert.Single(result);
        }

        [Fact]
        public async Task FetchFilteredReviewsAsync_ReturnsEmpty()
        {
            _mockReviewManagement.Setup(x => x.FetchFilteredReviewsAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Review>());

            var result = await _mockReviewManagement.Object.FetchFilteredReviewsAsync("nonexistentfilter");

            Assert.Empty(result);
        }


    }
}