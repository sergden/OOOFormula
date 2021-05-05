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
    public class IndexPageTests
    {
        [Fact]
        public void OnGet_ReturnGallery()
        {
            //Arrange
            var mockGalleryRep = new Mock<IGalleryRepository>(); //типизация 
            mockGalleryRep.Setup(_db => _db.GetAllGallery()).Returns(MockGallery.GetAllGalleryTest()); //настрока: вызов нужного метода и передача данных
            var pageModel = new IndexModel(mockGalleryRep.Object); //передача странице псевдообъекта

            //Act
            pageModel.OnGet(); //вызов метода

            //Assert
            var actualGallery = Assert.IsAssignableFrom<IQueryable<Gallery>>(pageModel.Gallery); //проверка Передается ли в представление в качестве модели объект IQueryable<Gallery>
            Assert.Equal(MockGallery.GetAllGalleryTest().Count(), actualGallery.Count()); //проверка количества объектов, которое передается в представление
        }
    }
}
