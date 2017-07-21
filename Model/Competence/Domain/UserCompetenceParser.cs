using System.Linq;

namespace Model.Competence.Domain
{
    public class UserCompetenceParser
    {
        public string[] ParseCompetenceText(string compentenceText)
        {
            return compentenceText
                .Split(' ', ',')
                .Where(x=>x != null)
                .Where(x=>x.StartsWith("#"))
                .Where(x=>x.Length >= 2)
                .Select(x=>x.Substring(1))
                .ToArray();
        }
    }
}