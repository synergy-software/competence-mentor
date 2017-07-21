using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Competence.Domain;

namespace Web.Tests.Competence.Domain
{
    [TestClass]
    public class SynonymsTest
    {
        [TestMethod]
        public void Test1()
        {
            var synonyms = new Synonyms(new Dictionary<string, string>());
            synonyms.Add("mssql", "ms-sql");
            var result = synonyms.FindCoreSynonym("mssql");
            Assert.AreEqual("mssql", result);
        }

        [TestMethod]
        public void Test2()
        {
            var synonyms = new Synonyms(new Dictionary<string, string>());
            synonyms.Add("mssql", "ms-sql");
            var result = synonyms.FindCoreSynonym("mssql");
            Assert.AreEqual("mssql", result);
            result = synonyms.FindCoreSynonym("ms-sql");
            Assert.AreEqual("mssql", result);
        }

        [TestMethod]
        public void TestConventions()
        {
            var synonyms = new Synonyms(new Dictionary<string, string>());
            Assert.AreEqual("mssql", synonyms.FindCoreSynonym("mssql "));
            Assert.AreEqual("mssql", synonyms.FindCoreSynonym("MsSql"));
            Assert.AreEqual("ms sql", synonyms.FindCoreSynonym("ms-SQL"));
            Assert.AreEqual("ms sql", synonyms.FindCoreSynonym("[ms sql]"));
        }
    }
}