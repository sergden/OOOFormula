using Microsoft.AspNetCore.Http;

namespace OOOFormula.Services
{
    public interface IFilesRepository
    {
        public bool checkMIMEType(IFormFile photo);

        public bool checkMIMEType(IFormCollection files);

        public void deleteFile(string Image, string Folder);

        public string UploadFile(IFormFile photo, string Folder);

        public string UploadFile(IFormCollection photo, string Folder);
    }
}
