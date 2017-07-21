namespace Model.Competence.Domain
{
    public interface IUserCompentencePersister
    {
        void Store(UserCompetence aggregate, CompentenceUpdateCommand command);
    }
}