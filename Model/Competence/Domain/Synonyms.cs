using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model.Competence.Domain
{
    public class Synonyms
    {
        public Synonyms(Dictionary<string, string> state)
        {
            synonymDictionary = state;
        }
        private readonly Dictionary<string, string> synonymDictionary = new Dictionary<string, string>();

        public string FindCoreSynonym(string competenceName)
        {
            competenceName = competenceName.Trim();
            competenceName = competenceName.Replace("-", " ");
            competenceName = competenceName.ToLower();
            competenceName = competenceName.TrimStart('[').TrimEnd(']');

            string coreSynonym;
            return synonymDictionary.TryGetValue(competenceName, out coreSynonym) ? coreSynonym : competenceName;
        }

        public void Add(string coreWord, string wordAdditional)
        {
            synonymDictionary[wordAdditional] = coreWord;
            if (wordAdditional.Contains('-'))
                Add(coreWord, wordAdditional.Replace("-", " "));
        }
    }
}
