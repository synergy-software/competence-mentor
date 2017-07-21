using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Competence.Infrastructure;
using Model.Infrastructure;

namespace Model.Competence.Domain
{
    public class Factory
    {
        private static ChartManager manager;

        public static ChartManager GetChartManager()
        {
            if (manager != null)
                return manager;

            manager = new ChartManager();

            var commands = CommandStore.ReadAll();
            foreach (var commandEnvelope in commands)
            {

                switch (commandEnvelope.CommandType)
                {
                    case Commands.CompetenceUpdateCommandType:
                        var command = commandEnvelope.GetCommand<CompetenceUpdateCommand>();
                        manager.UserCompetenceChange(command);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return manager;
        }

        private static Synonyms synonyms;

        public static Synonyms GetSynonyms()
        {
            if (synonyms != null)
                return synonyms;

            synonyms = new Synonyms();

            return synonyms;
        }
    }
}
