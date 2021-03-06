using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace OOOFormula.Services
{
    public class FilesRepository : IFilesRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilesRepository(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public static string Types { get; } = ".png, .jpg, .jpeg, .gif";

        public bool CheckMIMEType(IFormFile photo) //проверка типа файла
        {
            if (!photo.ContentType.Contains("image"))
            {
                return false;
            }
            return true;
        }

        public bool CheckMIMEType(IFormCollection files)
        {
            return true;
        }

        public void DeleteFile(string Image, string Folder)
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Folder, Image); //создаем полное имя файла

            if (Image != "noimage.png") //проверяем, не используется ли заглушка
            {
                File.Delete(filePath);
            }
        }

        public string UploadFile(IFormFile photo, string Folder)
        {
            string uniqueFileName = null;
            if (photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", Folder); //webRootPath возвращает путь до каталогаа wwwroot
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName; //генерация уникального имени файла
                string filePath = Path.Combine(uploadsFolder, uniqueFileName); //объединение имени файла и сгенерированного уникального имени

                //логика сохранения на сервер фото
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fs);
                }
            }

            return uniqueFileName;
        }

        public string UploadFile(IFormCollection photo, string Folder)
        {
            return "d";
        }

    }
}
