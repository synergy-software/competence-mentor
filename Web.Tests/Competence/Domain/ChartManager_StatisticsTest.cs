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
    public class ChartManager_StatisticsTest
    {
        [TestInitialize]
        public void Init()
        {
            new CompetencyAggregator().ResetDuringTests();
        }

        [TestMethod]
        public void None()
        {
            var charts = new CompetencyAggregator();
            var result = charts.GetStatistics();
            Assert.IsTrue(result.Length == 0);
        }

        [TestMethod]
        public void TestIncrease()
        {
            var charts = new CompetencyAggregator();
            charts.UserCompetenceChange(new CompetenceUpdateCommand {UserId = "1", Competencies = new[] {"comp1", "comp2"}});
            charts.UserCompetenceChange(new CompetenceUpdateCommand {UserId = "2", Competencies = new[] {"comp1", "comp3"}});
            var result = charts.GetStatistics();
            var numbers = result.OrderBy(x => x.Competence).Select(x => x.Count).ToArray();
            CollectionAssert.AreEquivalent(new [] {2,1,1}, numbers);
        }

        [TestMethod]
        public void TestIncreaseDecrease()
        {
            var charts = new CompetencyAggregator();
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId="1", Competencies = new[] { "comp1", "comp2" } });
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId="1", Competencies = new string[] {} });
            var result = charts.GetStatistics();
            Assert.AreEqual(0, result.Length);
        }
    }
}
