using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading;

namespace Model.Competence.Domain
{
    public class ChartManager
    {
        // nazwa kompentecji -> ilość osób
        private static readonly Dictionary<string, List<string>> userCompetencies = new Dictionary<string, List<string>>();
        private static readonly object SyncRoot = new object();


        public void UserCompetenceChange(UserCompetence userCompetence, CompetenceUpdateCommand command)
        {
            var userId = userCompetence.GetId();

            lock (SyncRoot)
            {
                ClearUserCompetencies(userId);
                foreach (var competency in command.Competencies)
                {
                    List<string> list;
                    if (userCompetencies.TryGetValue(competency, out list) == false)
                    {
                        list = new List<string>();
                        userCompetencies[competency] = list;
                    }

                    list.Add(userId);
                }
            }
        }

        private void ClearUserCompetencies(string userId)
        {
            lock (SyncRoot)
            {
                foreach (var userCompetencyList in userCompetencies.Values)
                {
                    userCompetencyList.Remove(userId);
                }
            }
        }

        public CompetenceSummary[] GetStatistics()
        {
            lock (SyncRoot)
            {
                return
                    userCompetencies
                        .Select(x => new CompetenceSummary {Competence = x.Key, Count = x.Value.Count})
                        .ToArray();
            }
        }

        public void ResetDuringTests()
        {
            lock (SyncRoot)
            {
                userCompetencies.Clear();
            }
        }
    }

    public class CompetenceSummary
    {
        public string Competence;
        public int Count;
    }
}