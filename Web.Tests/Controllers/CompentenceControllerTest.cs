﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            string result = controller.Get("user1");

            // Assert
            Assert.AreEqual("value", result);
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
    }
}
