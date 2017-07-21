using Model.Competence.Domain;
using Newtonsoft.Json;

namespace Model.Competence.Infrastructure
{
    public class UserCompentencePersister : IUserCompentencePersister
    {
        public void Store(UserCompetence aggregate, CompetenceUpdateCommand command)
        {
            var aggregateId = aggregate.GetId();
            var json = JsonConvert.SerializeObject(command);

        }
    }

    //public class CommandEnvelope
}