using Xunit;
using Moq;
using AniX_Shared.Interfaces;
using System;
using System.Threading.Tasks;
using System.IO;
using Assert = Xunit.Assert;

namespace AniX_UnitTest
{
    public class AzureBlobServiceTests
    {
        private readonly Mock<IAzureBlobService> _mockAzureBlobService;

        public AzureBlobServiceTests()
        {
            _mockAzureBlobService = new Mock<IAzureBlobService>();
        }

        [Fact]
        public async Task UploadImageAsync_ReturnsUri()
        {
            var mockStream = new MemoryStream();
            string userId = "user1";
            string contentType = "image/jpeg";
            string expectedUri = "http://example.com/blobs/user1/image.jpg";

            _mockAzureBlobService.Setup(service => service.UploadImageAsync(It.IsAny<Stream>(), userId, contentType))
                .ReturnsAsync(expectedUri);

            var result = await _mockAzureBlobService.Object.UploadImageAsync(mockStream, userId, contentType);

            Assert.Equal(expectedUri, result);
        }

        [Fact]
        public async Task UploadAnimeImageAsync_ReturnsUri()
        {
            var mockStream = new MemoryStream();
            string animeId = "anime1";
            string contentType = "image/jpeg";
            string expectedUri = "http://example.com/blobs/anime1/image.jpg";

            _mockAzureBlobService.Setup(service => service.UploadAnimeImageAsync(It.IsAny<Stream>(), animeId, contentType))
                .ReturnsAsync(expectedUri);

            var result = await _mockAzureBlobService.Object.UploadAnimeImageAsync(mockStream, animeId, contentType);

            Assert.Equal(expectedUri, result);
        }


        [Fact]
        public async Task DeleteImageAsync_PerformsDeletion()
        {
            string blobName = "blobToDelete.jpg";

            await _mockAzureBlobService.Object.DeleteImageAsync(blobName);

        }
        [Fact]
        public void GetImageUri_ReturnsUri()
        {
            string blobName = "image.jpg";
            string expectedUri = "http://example.com/blobs/image.jpg";

            _mockAzureBlobService.Setup(service => service.GetImageUri(blobName))
                .Returns(expectedUri);

            var result = _mockAzureBlobService.Object.GetImageUri(blobName);

            Assert.Equal(expectedUri, result);
        }

    }
}