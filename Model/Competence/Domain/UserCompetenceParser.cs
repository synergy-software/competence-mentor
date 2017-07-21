using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Model.Competence.Domain
{
    public class UserCompetenceParser
    {
        public string[] ParseCompetenceText(string compentenceText)
        {
            List<string> result = new List<string>();
            var regex = new Regex(@"\S*#(?:\[[^\]]+\]|\S+)");
            foreach (Match match in regex.Matches(compentenceText))
            {
                result.Add(match.Value.Substring(1).ToLower());
            }

            return result.Distinct().ToArray();
        }
    }
}