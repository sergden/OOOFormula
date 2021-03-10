using OOOFormula.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IGalleryRepository
    {
        IQueryable<Gallery> GetAllGallery();

        Task<Gallery> GetGallery(int id);

        Task<Gallery> Update(Gallery UpdatedGallery);

        Task<Gallery> Add(Gallery NewGallery);

        Task<Gallery> Delete(int id);

        public bool GalleryExists(int id);
    }
}
