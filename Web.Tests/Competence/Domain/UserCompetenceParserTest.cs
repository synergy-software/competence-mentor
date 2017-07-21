using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Competence.Domain;

namespace Web.Tests.Competence.Domain
{
    [TestClass]
    public class UserCompetenceParserTest
    {
        [TestMethod]
        public void One()
        {
            var parser = new UserCompetenceParser();
            var result = parser.ParseCompetenceText("#kompetence1");
            Assert.AreEqual(result.Single(), "#kompetence1");
        }
    }
}
