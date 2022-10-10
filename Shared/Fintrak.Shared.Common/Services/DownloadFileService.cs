using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Net.Http.Headers;
using System.Web;

namespace Fintrak.Shared.Common.Services
{
    public class DownloadFileService
    {
        public static HttpResponseMessage DownloadFile(string downloadFilePath, string fileName)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            FileInfo file = new FileInfo(downloadFilePath + "data.zip");

            if (Directory.Exists(downloadFilePath + "ExportedData\\"))
            {

                FastZip fz = new FastZip();
                string filetype = ""; // @"\.txt$"; // Only files ending in ".txt"
                fz.CreateZip(file.FullName, downloadFilePath + "ExportedData\\", true, filetype);
                //Copy the source file stream to MemoryStream and close the file stream
                MemoryStream responseStream = new MemoryStream();
                //Stream fileStream = new FileStream(Path.Combine(downloadFilePath, fileName), FileMode.Open, System.IO.FileAccess.ReadWrite);
                Stream fileStream = File.Open(file.FullName, FileMode.Open);
                fileStream.CopyTo(responseStream);
                fileStream.Close();


                responseStream.Position = 0;

                response.StatusCode = HttpStatusCode.OK;

                //Write the memory stream to HttpResponseMessage content
                response.Content = new StreamContent(responseStream); //Content = new ByteArrayContent(content.ToArray())
                string contentDisposition = string.Concat("attachment; filename=", fileName);
                response.Content.Headers.ContentDisposition =
                              ContentDispositionHeaderValue.Parse(contentDisposition);

                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(ReturnExtension(file.Extension));
            }

            return response;
        }

        private static string ReturnExtension(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                    return "application/ms-word";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
