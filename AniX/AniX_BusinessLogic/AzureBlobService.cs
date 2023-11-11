using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using AniX_Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using AniX_Utility;

namespace AniX_BusinessLogic
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly IErrorLoggingService _errorLoggingService;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureBlobService(IConfiguration configuration, IErrorLoggingService errorLoggingService)
        {
            var connectionString = configuration["AzureBlobStorage:ConnectionString"];
            _containerName = configuration["AzureBlobStorage:ContainerName"];

            _blobServiceClient = new BlobServiceClient(connectionString);
            _errorLoggingService = errorLoggingService;
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string userId, string contentType)
        {
            var fileExtension = contentType switch
            {
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/jpg" => ".jpeg",
                "image/gif" => ".gif",
                _ => null
            };

            if (fileExtension == null)
            {
                var errorMessage = $"Unsupported content type: {contentType}";
                await _errorLoggingService.LogErrorAsync(new ArgumentException(errorMessage, nameof(contentType)));
                throw new ArgumentException(errorMessage, nameof(contentType));
            }

            var blobName = $"profile_{userId}_{Guid.NewGuid()}{fileExtension}";
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            try
            {
                await blobClient.UploadAsync(imageStream, new BlobHttpHeaders { ContentType = contentType });
                return blobClient.Uri.AbsoluteUri;
            }
            catch (RequestFailedException ex)
            {
                await _errorLoggingService.LogErrorAsync(ex);
                throw new InvalidOperationException("Error uploading image to Azure Blob Storage.", ex);
            }
            catch (Exception ex)
            {
                await _errorLoggingService.LogErrorAsync(ex);
                throw new InvalidOperationException("An error occurred while uploading the image.", ex);
            }
        }

        public async Task<string> UploadAnimeImageAsync(Stream imageStream, string animeId, string contentType)
        {
            var fileExtension = contentType switch
            {
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/jpg" => ".jpeg",
                "image/gif" => ".gif",
                _ => null
            };

            if (fileExtension == null)
            {
                var errorMessage = $"Unsupported content type: {contentType}";
                await _errorLoggingService.LogErrorAsync(new ArgumentException(errorMessage, nameof(contentType)));
                throw new ArgumentException(errorMessage, nameof(contentType));
            }

            var blobName = $"anime_{animeId}_{Guid.NewGuid()}{fileExtension}";
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            try
            {
                await blobClient.UploadAsync(imageStream, new BlobHttpHeaders { ContentType = contentType });
                return blobClient.Uri.AbsoluteUri;
            }
            catch (RequestFailedException ex)
            {
                await _errorLoggingService.LogErrorAsync(ex);
                throw new InvalidOperationException("Error uploading image to Azure Blob Storage.", ex);
            }
            catch (Exception ex)
            {
                await _errorLoggingService.LogErrorAsync(ex);
                throw new InvalidOperationException("An error occurred while uploading the image.", ex);
            }
        }

        public async Task DeleteImageAsync(string imageUrl)
        {
            Uri uri = new Uri(imageUrl);
            string blobName = uri.Segments[^1];

            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            try
            {
                await blobClient.DeleteIfExistsAsync();
            }
            catch (RequestFailedException ex)
            {
                await _errorLoggingService.LogErrorAsync(ex);
                throw new InvalidOperationException("Error deleting image from Azure Blob Storage.", ex);
            }
            catch (Exception ex)
            {
                await _errorLoggingService.LogErrorAsync(ex);
                throw new InvalidOperationException("An error occurred while deleting the image.", ex);
            }
        }

        public string GetImageUri(string blobName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            return blobClient.Uri.AbsoluteUri;
        }
    }
}
