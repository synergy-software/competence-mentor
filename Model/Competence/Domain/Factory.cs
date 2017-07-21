using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Competence.Infrastructure;
using Model.Infrastructure;
using Newtonsoft.Json;

namespace Model.Competence.Domain
{
    public class Factory
    {
        private static CompetencyAggregator manager;

        public static CompetencyAggregator GetChartManager()
        {
            if (manager != null)
                return manager;

            manager = new CompetencyAggregator();

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

        public static string SynonymsFilePath { get; set; }

        private static Synonyms synonyms;

        public static Synonyms GetSynonyms()
        {
            if (synonyms != null)
                return synonyms;

            var synonymDictionary = new Dictionary<string, string>();
            if (SynonymsFilePath != null)
            {
                if (File.Exists(SynonymsFilePath))
                {
                    var json = File.ReadAllText(SynonymsFilePath);
                    synonymDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
            }
            synonyms = new Synonyms(synonymDictionary);

            return synonyms;
        }
    }
}
