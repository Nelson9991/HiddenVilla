using System;
using System.IO;
using System.Threading.Tasks;
using HiddenVilla_Server.Service.IService;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HiddenVilla_Server.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _contextAccessor;

        public FileUpload(IWebHostEnvironment hostEnvironment, IHttpContextAccessor contextAccessor)
        {
            _hostEnvironment = hostEnvironment;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                var fileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;

                var folderDiretory = $"{_hostEnvironment.WebRootPath}\\roomImages";
                var path = Path.Combine(folderDiretory, fileName);

                await using var ms = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(ms);

                if (!Directory.Exists(folderDiretory))
                {
                    Directory.CreateDirectory(folderDiretory);
                }

                await using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                ms.WriteTo(fs);

                var url =
                    $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host.Value}/";
                var fullPath = $"{url}roomImages/{fileName}";
                return fullPath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                var path = $"{_hostEnvironment.WebRootPath}\\roomImages\\{fileName}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}