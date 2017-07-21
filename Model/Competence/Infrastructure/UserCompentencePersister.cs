using Model.Competence.Domain;
using Newtonsoft.Json;

namespace Model.Competence.Infrastructure
{
    public class UserCompentencePersister : IUserCompentencePersister
    {
        public void Store(UserCompetence aggregate, CompentenceUpdateCommand command)
        {
            var json = JsonConvert.SerializeObject(command);

        }
    }
}