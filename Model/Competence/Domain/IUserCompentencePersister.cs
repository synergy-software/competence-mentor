namespace Model.Competence.Domain
{
    public interface IUserCompentencePersister
    {
        void Store(UserCompetence aggregate, CompetenceUpdateCommand command);
    }
}