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

namespace OOOFormula.Tests.UnitTests
{
    public class ContantsPageTests
    {
        [Fact]
        public async Task OnGet_ReturnPage()
        {
            //Arrange
            var mockPagesRep = new Mock<IPagesRepository>(); //типизация 
            mockPagesRep.Setup(_db => _db.GetAllPages()).Returns(MockPages.GetAllPagesTest()); //настрока: вызов нужного метода и передача данных
            var pageModel = new ContactsModel(new Mock<IRequestsRepository>().Object, new Mock<IGoogleRecaptchaRepository>().Object, mockPagesRep.Object); //передача странице псевдообъекта

            //Act
            var result = await pageModel.OnGet();

            //Assert
            Assert.IsType<PageResult>(result);
        }
        
        [Fact]
        public async Task OnGet_ReturnNotFound()
        {
            //Arrange
            var mockPagesRep = new Mock<IPagesRepository>(); //типизация 
            mockPagesRep.Setup(_db => _db.GetAllPages()).Returns(MockPages.GetAllPagesTest()); //настрока: вызов нужного метода и передача данных
            var pageModel = new ContactsModel(new Mock<IRequestsRepository>().Object, new Mock<IGoogleRecaptchaRepository>().Object, mockPagesRep.Object); //передача странице псевдообъекта

            //Act
            var result = await pageModel.OnGet();

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
