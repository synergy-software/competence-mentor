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
            Assert.AreEqual(result.Single(), "kompetence1");
        }

        [TestMethod]
        public void Many()
        {
            var parser = new UserCompetenceParser();
            var result = parser.ParseCompetenceText("To jest #kompetence1 #parser #c# #.net @con=][p0");
            CollectionAssert.AreEquivalent(result, new string[] { "kompetence1", "parser", "c#", ".net" });
        }

        [TestMethod]
        public void Invalid()
        {
            var parser = new UserCompetenceParser();
            var result = parser.ParseCompetenceText("#");
            Assert.IsTrue(result.Length == 0);
        }

        [TestMethod]
        public void Real1()
        {
            var parser = new UserCompetenceParser();
            var result = parser.ParseCompetenceText("Passionate #C# & #ASP.NET developer. My knowledge is confirmed by working in big projects for huge clients. I am very eager to learn new things and always looking for new opportunities (only remote). Specialties: #C# and #ASP.NET programming, #OO-analysis and #design");
            CollectionAssert.AreEquivalent(result, new string[] { "c#", "asp.net", "oo-analysis", "design" });
        }

        [TestMethod]
        public void Real2()
        {
            var parser = new UserCompetenceParser();
            var result = parser.ParseCompetenceText(@"I was
a #.net developer, #software-architect, #team-lead, #project-manager, #research team lead, #QA tools developer, #support coordinator (service manager), #product-management team member, #business-analyst, #technology-trainer, #recruiter and #team-builder, production automation coordinator (templates, knowledge base), #database-designer, #release-manager.

I will be 
a software designer or team lead focused on building state-of-art, creative solutions using the most #efficient-processes.

Out of the technologies I know, these are my favourite when building #enterprise-applications: 
#WWF, #WCF, #ASP.NET #MVC, #CSS (plus #less-CSS), #jQuery, #HTML5, #NHibernate, #[SQL Server] db, #[SQL Server Integration Services], #[SQL Server Analysis Services], #log4net, #AutoMapper, #Castle (#Windsor), #NUnit + #Moq");
            Console.WriteLine(string.Join(" | ", result));
        }

        


    }
}
