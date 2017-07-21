using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Competence.Domain;
using Model.Competence.Infrastructure;

namespace Web.Tests.Competence.Domain
{
    [TestClass]
    public class UserCompentenceTest
    {
        [TestMethod]
        public void UpdateAndRead()
        {
            var repository = new UserCompetenceRepository();
            var entity = repository.Get("user1");
            entity.UpdateCompentence( new CompentenceUpdateCommand
            {
                CompentenceText = "lala"
            
            });
            Assert.AreEqual("lala", entity.GetCompetenceText());
        }
    }
}
