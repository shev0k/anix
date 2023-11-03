using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using AniX_Shared.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AniX_BusinessLogic
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureBlobService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureBlobStorage:ConnectionString"];
            _containerName = configuration["AzureBlobStorage:ContainerName"];

            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string userId, string contentType)
        {
            var fileExtension = contentType switch
            {
                "image/jpeg" => ".jpg",
                "image/png" => ".png",
                "image/jpg" => ".jpg",
                _ => throw new ArgumentException("Unsupported content type.", nameof(contentType))
            };

            var blobName = $"profile_{userId}_{Guid.NewGuid()}{fileExtension}";
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            try
            {
                await blobClient.UploadAsync(imageStream, new BlobHttpHeaders { ContentType = contentType }, conditions: null);
                return blobClient.Uri.AbsoluteUri;
            }
            catch (RequestFailedException ex)
            {
                throw new InvalidOperationException("Error uploading image to Azure Blob Storage.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while uploading the image.", ex);
            }
        }


        public async Task DeleteImageAsync(string blobName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            try
            {
                await blobClient.DeleteIfExistsAsync();
            }
            catch (RequestFailedException ex)
            {
                throw new InvalidOperationException("Error deleting image from Azure Blob Storage.", ex);
            }
            catch (Exception ex)
            {
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
