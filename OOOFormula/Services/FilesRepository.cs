using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

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

        public bool CheckMIMEType(IFormFileCollection files)
        {
            foreach (var file in files)
            {
                if (!file.ContentType.Contains("image"))
                {
                    return false;
                }
            }
            return true;
        }

        public void DeleteFile(string Image, string Folder)
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", Folder, Image); //создаем полное имя файла
            if (Image != "noimage.png")//проверяем, не используется ли заглушка
            {
                if (File.Exists(filePath)) //проверяем существует ли файл
                {
                    File.Delete(filePath);
                }
            }
        }

        public async Task<string> UploadFile(IFormFile photo, string Folder, string subFolder = null)
        {
            string uniqueFileName = null;
            if (photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", Folder, subFolder); //webRootPath возвращает путь до каталога wwwroot
                uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName; //генерация уникального имени файла
                string filePath = Path.Combine(uploadsFolder, uniqueFileName); //объединение имени файла и сгенерированного уникального имени
                if (!Directory.Exists(uploadsFolder)) //проверяем существует ли каталог
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                //логика сохранения на сервер фото
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fs);
                }
            }
            return uniqueFileName;
        }
    }
}
