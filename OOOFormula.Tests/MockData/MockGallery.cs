using OOOFormula.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OOOFormula.Data;

using System.Threading.Tasks;

namespace OOOFormula.Tests.MockData
{
    public class MockGallery
    {
        public static List<Gallery> GetOneGalleryTest(int id = 1)
        {
            var gallery = new List<Gallery>
            {
                new Gallery
                {
                Id=id,
                Name="Item_1",
                Description="",
                ImagePath="img.jpg",
                Status=true,
                DateAdd= new DateTime(2021,5,5)
                }
            };

            return gallery;
        }


        public static IQueryable<Gallery> GetAllGalleryTest()
        {
            return GetOneGalleryTest().AsQueryable();
        }

        public static IQueryable<Gallery> SearchGalleryTest(string searchString)
        {
            List<Gallery> Gal = GetOneGalleryTest();
            return Gal.Where(p =>
                     p.Name.ToLower().Contains(searchString.ToLower()))
                .AsQueryable();
        }
    }
}
