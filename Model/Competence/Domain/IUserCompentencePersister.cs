namespace Model.Competence.Domain
{
    public interface IUserCompentencePersister
    {
        void Store(CompentenceUpdateCommand command);
    }

    public class UserCompentencePersister : IUserCompentencePersister
    {
        public void Store(CompentenceUpdateCommand command)
        {
        }
    }
}