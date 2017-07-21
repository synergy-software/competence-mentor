using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Competence.Domain;

namespace Web.Tests.Competence.Domain
{
    [TestClass]
    public class UserCompentenceTest
    {
        [TestMethod]
        public void UpdateAndRead()
        {
            var entity = new UserCompetence("user1", new UserCompentencePersister());
            entity.UpdateCompentence( new CompentenceUpdateCommand
            {
                CompentenceText = "lala"
            
            });
            Assert.AreEqual("lala", entity.GetCompetenceText());
        }
    }
}
