using System.Linq;

namespace Model.Competence.Domain
{
    public class UserCompetenceParser
    {
        public string[] ParseCompetenceText(string compentenceText)
        {
            return compentenceText
                // TODO na razie wersja naiwna
                .Split(' ')
                .Where(x=>x != null)
                .Where(x=>x.StartsWith("#"))
                .ToArray();
        }
    }
}