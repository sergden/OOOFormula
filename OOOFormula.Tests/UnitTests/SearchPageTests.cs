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
using Xunit;
using OOOFormula.Pages;
using OOOFormula.Data;
using OOOFormula.Services;
using OOOFormula.Models;
using OOOFormula.Tests.MockData;
using OOOFormula.Pages.Catalog;

namespace OOOFormula.Tests.UnitTests
{
    public class SearchPageTests
    {
        [Theory]
        [InlineData("String_1")]
        [InlineData("String_2")]
        [InlineData("String_3")]
        [InlineData("String_4")]
        public async Task OnGet_SearchReturnPage(string SearchString)
        {
            //Arrange
            var mockGalleryRep = new Mock<IGalleryRepository>();
            mockGalleryRep.Setup(_db => _db.SearchGallery(SearchString)).Returns(MockGallery.SearchGalleryTest(SearchString)); //настрока: вызов нужного метода и передача данных
            var mockProductsRep = new Mock<IProductsRepository>();
            mockProductsRep.Setup(_db => _db.SearchProduct(SearchString)).Returns(MockProducts.SearchProductsTest(SearchString)); //настрока: вызов нужного метода и передача данных
            var pageModel = new SearchModel(mockProductsRep.Object, mockGalleryRep.Object);

            //Act
            var result = await pageModel.OnGetAsync(SearchString);

            //Assert
            Assert.IsType<PageResult>(result);
        }      
        
        
        [Theory]
        [InlineData(null)]
        public async Task OnGet_SearchReturnNotFound(string SearchString)
        {
            //Arrange
            var mockGalleryRep = new Mock<IGalleryRepository>();
            mockGalleryRep.Setup(_db => _db.SearchGallery(SearchString)).Returns(MockGallery.SearchGalleryTest(SearchString)); //настрока: вызов нужного метода и передача данных
            var mockProductsRep = new Mock<IProductsRepository>();
            mockProductsRep.Setup(_db => _db.SearchProduct(SearchString)).Returns(MockProducts.SearchProductsTest(SearchString)); //настрока: вызов нужного метода и передача данных
            var pageModel = new SearchModel(mockProductsRep.Object, mockGalleryRep.Object);

            //Act
            var result = await pageModel.OnGetAsync(SearchString);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
