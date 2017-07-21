using Model.Competence.Domain;

namespace Model.Competence.Infrastructure
{
    public class UserCompetenceRepository
    {
        public UserCompetence Get(string userId)
        {
            var persister = new UserCompentencePersister();

            return new UserCompetence(userId, persister);
        }
    }
}