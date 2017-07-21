using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Competence.Domain;
using Model.Competence.Infrastructure;

namespace Web.Tests.Competence.Domain
{
    [TestClass]
    public class UserCompentenceTest
    {

        UserCompentenceMockPersister persister;
        UserCompetence entity;

        [TestInitialize]
        public void Init()
        {
            persister = new UserCompentenceMockPersister();
            entity = new UserCompetence("user1", persister);
        }

        [TestMethod]
        public void NoCompetence()
        {
            entity.UpdateCompentence(new CompetenceUpdateCommand
            {
                CompentenceText = "lala"
            });

            Assert.IsTrue(persister.List.Single().command.Competencies.Length == 0);
        }

        [TestMethod]
        public void OneCompetence()
        {
            entity.UpdateCompentence(new CompetenceUpdateCommand
            {
                CompentenceText = "lala #komp1"
            });

            Assert.IsTrue(persister.List.Single().command.Competencies.Length == 1);
        }
    }

    public class UserCompentenceMockPersister : IUserCompentencePersister
    {
        public readonly List<Entry> List = new List<Entry>();

        public class Entry
        {
            public UserCompetence aggregate;
            public CompetenceUpdateCommand command;
        }
        public void Store(UserCompetence aggregate, CompetenceUpdateCommand command)
        {
            List.Add(new Entry {aggregate = aggregate, command = command});
        }
    }
}
