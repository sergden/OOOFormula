using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IFilesRepository
    {
        public bool CheckMIMEType(IFormFile photo);

        public bool CheckMIMEType(IFormFileCollection files);

        public void DeleteFile(string Image, string Folder, string subFolder = "");

        public Task<string> UploadFile(IFormFile photo, string Folder, string subFolder = "");
    }
}
