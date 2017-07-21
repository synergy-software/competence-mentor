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
    public class ChartManager_SearchTest
    {
        [TestInitialize]
        public void Init()
        {
            new ChartManager().ResetDuringTests();
        }

        [TestMethod]
        public void FindManyCompetencies()
        {
            var charts = new ChartManager();
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "1", Competencies = new[] { "comp1", "comp2" } });
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "2", Competencies = new[] { "comp1", "comp3" } });
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "3", Competencies = new[] { "comp2", "comp3" } });
            var result = charts.Search(new List<string> {"comp1", "non_exist"});
            CollectionAssert.AreEquivalent(new[] { "1", "2" }, result);
        }

        [TestMethod]
        public void FindManyCompetenciesAll()
        {
            var charts = new ChartManager();
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "1", Competencies = new[] { "comp1", "comp2" } });
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "2", Competencies = new[] { "comp1", "comp3" } });
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "3", Competencies = new[] { "comp2", "comp3" } });
            var result = charts.Search(new List<string> { "comp1", "comp3" });
            CollectionAssert.AreEquivalent(new[] { "1", "2", "3" }, result);
        }

        [TestMethod]
        public void FindOneCompetency()
        {
            var charts = new ChartManager();
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "1", Competencies = new[] { "comp1", "comp" } });
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "2", Competencies = new[] { "comp1", "comp3" } });
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "3", Competencies = new[] { "comp2", "comp3" } });
            var result = charts.Search(new List<string> { "comp" });
            CollectionAssert.AreEquivalent(new[] { "1" }, result);
        }

        [TestMethod]
        public void FindCompetenceByName()
        {
            var charts = new ChartManager();
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "1", Competencies = new[] { "comp1", "comp" } });
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "2", Competencies = new[] { "comp1", "comp3" } });
            charts.UserCompetenceChange(new CompetenceUpdateCommand { UserId = "3", Competencies = new[] { "comp2", "comp3", "inne" } });
            var result = charts.FindCompetenceByPrefixName("comp");
            CollectionAssert.AreEquivalent(new[] { "comp", "comp1", "comp2", "comp3" }, result);
        }

    }
}
