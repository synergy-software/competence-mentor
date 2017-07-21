using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;
using Model.Competence.Domain;
using Model.Competence.Infrastructure;

namespace Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CompetenceController : ApiController
    {
        // GET api/competence/{id}
        public UserCompetenceGetModel Get(string id)
        {
            var repo = new UserCompetenceRepository();
            var entity = repo.Get(id);
            
            return new UserCompetenceGetModel
            {
                CompetenceText = entity.CompetenceText,
                Competencies = entity.CompetenceList
            };
        }
        
        // POST api/competence
        public void Post(UserCompetenceUpdateModel updateModel)
        {
            var repo = new UserCompetenceRepository();
            var entity = repo.Get(updateModel.UserId);
            var command = new CompetenceUpdateCommand {CompentenceText = updateModel.CompetenceText};
            entity.UpdateCompentence(command);
        }


        [Route("api/competence/statistics")]
        [HttpGet]
        public CompetenceSummary[] Statistics()
        {
            return Factory.GetChartManager().GetStatistics();
        }


        [Route("api/competence/search")]
        [HttpGet]
        public string[] Search(string compentence)
        {
            var list = new List<string>();
            var regex = new Regex(@"(\[.*?\])");
            foreach (Match match in regex.Matches(compentence))
            {
                var value = match.Value;
                list.Add(value);
            }
            compentence = regex.Replace(compentence, "92AD4AE5-F98A-4897-A73F-B905CC224E9B");
            compentence = compentence.Replace("92AD4AE5-F98A-4897-A73F-B905CC224E9B", "");

            var competenceList = compentence.Trim().Split(' ');
            foreach (var competence in competenceList)
            {
                if (string.IsNullOrWhiteSpace(competence))
                    continue;
                list.Add(competence);
            }
            return Factory.GetChartManager().SearchUsers(list);
        }

        [Route("api/competence/name")]
        [HttpGet]
        public string[] FindCompetenceByName(string compentencePrefix)
        {
            compentencePrefix = Factory.GetSynonyms().FindCoreSynonym(compentencePrefix);
            return Factory.GetChartManager().FindCompetenceByPrefixName(compentencePrefix);
        }
    }

    public class UserCompetenceUpdateModel
    {
        public string UserId;
        public string CompetenceText;
    }

    public class UserCompetenceGetModel
    {
        //public string UserId;
        public string CompetenceText;
        public string[] Competencies;
    }
}
