using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniX_Shared.Interfaces
{
    public interface IAzureBlobService
    {
        Task<string> UploadImageAsync(Stream imageStream, string userId, string contentType);
        Task<string> UploadAnimeImageAsync(Stream imageStream, string animeId, string contentType);
        Task DeleteImageAsync(string blobName);
        string GetImageUri(string blobName);
    }
}
