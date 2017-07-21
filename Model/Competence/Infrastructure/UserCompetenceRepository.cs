using Model.Competence.Domain;
using Model.Infrastructure;

namespace Model.Competence.Infrastructure
{
    public class UserCompetenceRepository
    {
        public UserCompetence Get(string userId)
        {
            var persister = new UserCompentencePersister();
            var commands = CommandStore.ReadFor(userId);
            var state = new UserCompetence.State(userId);
            foreach (var commandEnvelope in commands)
            {
                if (commandEnvelope.CommandType == typeof(CompetenceUpdateCommand).FullName)
                {
                    var command = commandEnvelope.GetCommand<CompetenceUpdateCommand>();
                    state.Apply(command);
                }
            }


            return new UserCompetence(state, persister);
        }
    }
}