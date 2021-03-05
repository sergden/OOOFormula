using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IFileRepository
    {
        public bool checkMIMEType(IFormFile photo);

        public bool checkMIMEType(IFormCollection files);

        public void deleteFile(string Image, string Folder);

        public string UploadFile(IFormFile photo, string Folder);

        public string UploadFile(IFormCollection photo, string Folder);
    }
}
