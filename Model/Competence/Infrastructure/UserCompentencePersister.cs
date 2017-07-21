using System;
using System.IO;
using Model.Competence.Domain;
using Model.Infrastructure;
using Newtonsoft.Json;

namespace Model.Competence.Infrastructure
{
    public class UserCompentencePersister : IUserCompentencePersister
    {
        public void Store(UserCompetence aggregate, CompetenceUpdateCommand command)
        {
            CommandStore.AppendToStream(aggregate.GetId(), command, Commands.CompetenceUpdateCommandType);
            
            Factory.GetChartManager().UserCompetenceChange(command);
        }
    }


}