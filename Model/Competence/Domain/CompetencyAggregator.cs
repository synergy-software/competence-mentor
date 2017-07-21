using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading;

namespace Model.Competence.Domain
{
    public class CompetencyAggregator
    {
        // nazwa kompentecji -> osoby
        private readonly Dictionary<string, List<string>> userCompetencies = new Dictionary<string, List<string>>();
        private static readonly object syncRoot = new object();

        public void UserCompetenceChange(CompetenceUpdateCommand command)
        {
            string userId = command.UserId;
            var factory = Factory.GetSynonyms();
            lock (syncRoot)
            {
                ClearUserCompetencies(userId);
                foreach (var competency in command.Competencies)
                {
                    var competencySynonym = factory.FindCoreSynonym(competency);
                    List<string> list;
                    if (userCompetencies.TryGetValue(competencySynonym, out list) == false)
                    {
                        list = new List<string>();
                        userCompetencies[competencySynonym] = list;
                    }

                    list.Add(userId);
                }
            }
        }

        private void ClearUserCompetencies(string userId)
        {
            lock (syncRoot)
            {
                foreach (var userCompetencyList in userCompetencies.Values)
                {
                    userCompetencyList.Remove(userId);
                }
            }
        }

        public CompetenceSummary[] GetStatistics()
        {
            lock (syncRoot)
            {
                return
                    userCompetencies
                        .Select(x => new CompetenceSummary {Competence = x.Key, Count = x.Value.Count})
                        .Where(x=>x.Count > 0)
                        .ToArray();
            }
        }

        public void ResetDuringTests()
        {
            lock (syncRoot)
            {
                userCompetencies.Clear();
            }
        }

        public string[] SearchUsers(List<string> competencies)
        {
            List<string> userListAll = new List<string>();
            foreach (var competence in competencies)
            {
                var competenceSynonym = Factory.GetSynonyms().FindCoreSynonym(competence);

                List<string> listUserIds;
                if (userCompetencies.TryGetValue(competenceSynonym, out listUserIds))
                {
                    userListAll.AddRange(listUserIds);
                }
            }
            return userListAll.Distinct().ToArray();
        }

        public string[] FindCompetenceByPrefixName(string compentencePrefix)
        {
            return userCompetencies.Keys.Where(x => x.StartsWith(compentencePrefix)).ToArray();
        }
    }

    public class CompetenceSummary
    {
        public string Competence;
        public int Count;
    }
}