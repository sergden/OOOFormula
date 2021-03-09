using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOFormula.Services
{
    public interface IGalleryRepository
    {
        IQueryable<Gallery> GetAllGallery();

        Gallery GetGallery(int id);

        Gallery Update(Gallery UpdatedGallery);

        Gallery Add(Gallery Newallery);

        Gallery Delete(int id);
    }
}
