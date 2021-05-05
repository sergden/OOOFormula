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
    public class CatalogPageTests
    {
        [Theory]
        [InlineData(0, 0, "")]
        public async Task OnGet_ReturnPaginatedList(decimal PriceFrom,
            decimal PriceTo, string searchString)
        {
            //Arrange
            var mockProductsRep = new Mock<IProductsRepository>();
            mockProductsRep.Setup(_db => _db.GetAllProducts()).Returns(MockProducts.GetAllProductsTest()); //настрока: вызов нужного метода и передача данных
            var pageModel = new CatalogModel(mockProductsRep.Object, new Mock<IMaterialsRepository>().Object, new Mock<IManufacturersRepostory>().Object);

            //Act
            var result = await pageModel.OnGetAsync(PriceFrom, PriceTo, SortState.NameAsc, null, null, 1, searchString);

            //Assert
            Assert.IsType<PaginatedList<Products>>(result);
        }
    }
}
