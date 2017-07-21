using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Model.Competence.Domain
{
    public class ChartManager
    {
        // nazwa kompentecji -> ilość osób
        private static readonly ConcurrentDictionary<string, int> counts = new ConcurrentDictionary<string, int>();

        public void UserCompetenceIncrease(UserCompetence userCompetence, CompetenceUpdateCommand command)
        {
            foreach (var competency in command.Competencies)
            {
                counts.AddOrUpdate(competency, s => 1, (s, i) => i + 1);
            }
        }

        public void UserCompetenceDecrease(UserCompetence userCompetence, CompetenceUpdateCommand command)
        {
            foreach (var competency in command.Competencies)
            {
                //lock (SyncRoot)
                //{
                //    int value;
                //    if (counts.TryGetValue(competency, out value) == true)
                //        counts[competency] = value - 1;
                //}

                counts.AddOrUpdate(competency, s => 0, (s, i) => i - 1);
            }
        }

        public CompetenceSummary[] GetStatistics()
        {
            return counts.Select(x => new CompetenceSummary { Competence = x.Key, Count = x.Value}).ToArray();
        }

        public void ResetDuringTests()
        {
            counts.Clear();
        }
    }

    public class CompetenceSummary
    {
        public string Competence;
        public int Count;
    }
}