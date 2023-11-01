using System;
using System.Threading.Tasks;
using AniX_Utility;

namespace FTPServiceTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FTPService ftpService = new FTPService();

            // Test uploading
            string localUploadPath = "C:\\file.txt";
            string remoteUploadPath = "remote/folder/file.txt";
            bool uploadSuccess = await ftpService.UploadFileAsync(localUploadPath, remoteUploadPath);
            Console.WriteLine($"Upload success: {uploadSuccess}");

            // Test downloading
            string remoteDownloadPath = "remote/folder/file.txt";
            string localDownloadPath = "C:\\file.txt";
            bool downloadSuccess = await ftpService.DownloadFileAsync(remoteDownloadPath, localDownloadPath);
            Console.WriteLine($"Download success: {downloadSuccess}");

            // Test deleting
            string remoteDeletePath = "remote/folder/file.txt";
            bool deleteSuccess = await ftpService.DeleteFileAsync(remoteDeletePath);
            Console.WriteLine($"Delete success: {deleteSuccess}");
        }
    }
}