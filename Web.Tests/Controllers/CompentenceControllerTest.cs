using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Infrastructure;
using Web.Controllers;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class CompentenceControllerTest
    {
        [TestMethod]
        public void GetById()
        {
            // Arrange
            CompetenceController controller = new CompetenceController();

            // Act
            var result = controller.Get("user1");

            // Assert
            Assert.AreEqual("tekst #kompetencja1 #kompetencja2", result.CompetenceText);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            CompetenceController controller = new CompetenceController();

            // Act
            //controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            CompetenceController controller = new CompetenceController();

            // Act
            //controller.Put("User1", "mój tekst #kompoent");

            // Assert
        }

        [TestMethod]
        public void Search()
        {
            // Arrange
            CommandStore.DatabasePath = AppDomain.CurrentDomain.BaseDirectory + @"\\..\\..\\..\\Web\\App_Data";

            var controller = new CompetenceController();

            // Act
            var result = controller.Search("[database designer] początek [dask asdfs] [pola]");

            // Assert
            Assert.AreEqual(2, result.Length);
        }
    }
}
