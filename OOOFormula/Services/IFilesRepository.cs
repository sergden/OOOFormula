using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace OOOFormula.Services
{
    public interface IFilesRepository
    {
        public bool CheckMIMEType(IFormFile photo);

        public bool CheckMIMEType(IFormCollection files);

        public void DeleteFile(string Image, string Folder);

        public void DeleteFile(List<string> Images, string Folder);

        public string UploadFile(IFormFile photo, string Folder);

        public string UploadFile(IFormCollection photo, string Folder);
    }
}
