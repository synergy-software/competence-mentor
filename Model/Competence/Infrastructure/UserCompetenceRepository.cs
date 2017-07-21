using System;
using Model.Competence.Domain;
using Model.Infrastructure;

namespace Model.Competence.Infrastructure
{
    public class UserCompetenceRepository
    {
        public UserCompetence Get(string userId)
        {
            var persister = new UserCompentencePersister();
            var state = new UserCompetence.State(userId);
            var commands = CommandStore.ReadFromStream(userId);
            foreach (var commandEnvelope in commands)
            {
                switch (commandEnvelope.CommandType)
                {
                    case Commands.CompetenceUpdateCommandType:
                        var command = commandEnvelope.GetCommand<CompetenceUpdateCommand>();
                        state.Apply(command);
                        break;
                    default:
                        throw new InvalidOperationException();
                }

            }


            return new UserCompetence(state, persister);
        }
    }
}