using Model.Competence.Domain;

namespace Model.Competence.Infrastructure
{
    public class UserCompetenceRepository
    {
        public UserCompetence Get(string userId)
        {
            return new UserCompetence(userId, new UserCompentencePersister());
        }
    }
}