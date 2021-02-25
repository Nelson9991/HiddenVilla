using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace HiddenVilla_Server.Service.IService
{
    public interface IFileUpload
    {
        public Task<string> UploadFile(IBrowserFile file);
        public bool DeleteFile(string fileName);
    }
}