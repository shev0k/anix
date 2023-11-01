using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AniX_Utility
{
    public class FTPService
    {
        private readonly string _ftpServerAddress = "luna.fhict.nl";
        private readonly int _ftpPort = 21;
        private readonly string _ftpBasePath = "/domains/i499309.luna.fhict.nl";
        private readonly string _ftpUser = "i499309";
        private readonly string _ftpPassword = "BCGclau18_*03";
    
        public FTPService()
        {
        }

        public async Task<bool> UploadFileAsync(string localFilePath, string remoteFilePath)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create($"ftp://{_ftpServerAddress}:{_ftpPort}{_ftpBasePath}/{remoteFilePath}");
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                request.EnableSsl = true;

                using (var streamReader = new StreamReader(localFilePath))
                {
                    byte[] fileContents = System.Text.Encoding.UTF8.GetBytes(await streamReader.ReadToEndAsync());
                    request.ContentLength = fileContents.Length;

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(fileContents, 0, fileContents.Length);
                    }
                }

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload Status: {response.StatusDescription}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DownloadFileAsync(string remoteFilePath, string localFilePath)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create($"ftp://{_ftpServerAddress}:{_ftpPort}{_ftpBasePath}/{remoteFilePath}");
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                request.EnableSsl = true;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    var content = await reader.ReadToEndAsync();
                    await File.WriteAllTextAsync(localFilePath, content);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteFileAsync(string remoteFilePath)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create($"ftp://{_ftpServerAddress}:{_ftpPort}{_ftpBasePath}/{remoteFilePath}");
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
                request.EnableSsl = true;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Delete Status: {response.StatusDescription}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}