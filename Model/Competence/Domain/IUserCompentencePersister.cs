namespace Model.Competence.Domain
{
    public interface IUserCompentencePersister
    {
        void Store(CompentenceUpdateCommand command);
    }
}