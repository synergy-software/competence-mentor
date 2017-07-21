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
    public class ChartManagerTest
    {
        [TestInitialize]
        public void Init()
        {
            new ChartManager().ResetDuringTests();
        }

        [TestMethod]
        public void None()
        {
            var charts = new ChartManager();
            var result = charts.GetStatistics();
            Assert.IsTrue(result.Length == 0);
        }

        [TestMethod]
        public void TestIncrease()
        {
            var charts = new ChartManager();
            charts.UserCompetenceIncrease(new UserCompetence("1", null), new CompetenceUpdateCommand {Competencies = new []{"1", "2"}});
            charts.UserCompetenceIncrease(new UserCompetence("1", null), new CompetenceUpdateCommand {Competencies = new []{"1", "3"}});
            var result = charts.GetStatistics();
            var numbers = result.OrderBy(x => x.Competence).Select(x => x.Count).ToArray();
            CollectionAssert.AreEquivalent(new [] {2,1,1}, numbers);
        }

        [TestMethod]
        public void TestIncreaseDecrease()
        {
            var charts = new ChartManager();
            charts.UserCompetenceIncrease(new UserCompetence("1", null), new CompetenceUpdateCommand { Competencies = new[] { "1", "2" } });
            charts.UserCompetenceDecrease(new UserCompetence("1", null), new CompetenceUpdateCommand { Competencies = new[] { "1", "2" } });
            var result = charts.GetStatistics();
            Assert.AreEqual(0, result.Single(x => x.Competence == "1").Count);
            Assert.AreEqual(0, result.Single(x => x.Competence == "2").Count);
        }
    }
}
