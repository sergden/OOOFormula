using Microsoft.AspNetCore.Http;
using OOOFormula.Models;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IGoogleRecaptchaRepository
    {
        Task<RecaptchaResponse> Validate(IFormCollection form);
    }
}
