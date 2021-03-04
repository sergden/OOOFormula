using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OOOFormula.Models
{
    public class MIMEType
    {
        public static string Types { get; } = ".png, .jpg, .jpeg, .gif";

        public static bool checkMIMEType(IFormFile file)
        {
            if (!file.ContentType.Contains("image"))
            {
                return false;
            }
            return true;
        }
    }
}
