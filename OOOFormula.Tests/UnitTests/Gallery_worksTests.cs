using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using OOOFormula.Pages;
using OOOFormula.Data;
using OOOFormula.Services;
using OOOFormula.Models;
using OOOFormula.Tests.MockData;

namespace OOOFormula.Tests.UnitTests
{
    public class Gallery_worksTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task OnGet_ReturnGalleryPaginated(int pageIndex)
        {
            //Arrange
            var mockGalleryRep = new Mock<IGalleryRepository>(); //типизация 
            mockGalleryRep.Setup(_db => _db.GetAllGallery()).Returns(MockGallery.GetAllGalleryTest()); //настрока: вызов нужного метода и передача данных
            var pageModel = new Gallery_worksModel(mockGalleryRep.Object);

            //Act
            await pageModel.OnGetAsync(pageIndex);

            //Assert
            var actualresult = Assert.IsAssignableFrom<PaginatedList<Gallery>>(pageModel.Gallery);
            Assert.IsType<PaginatedList<Gallery>>(actualresult);
        }
    }
}
