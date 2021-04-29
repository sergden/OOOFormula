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

        IQueryable<Gallery> SearchGallery(string searchString);

        IQueryable<Gallery> Sorting(IQueryable<Gallery> items, SortState? sortState);

        public bool GalleryExists(int id);
    }
}
