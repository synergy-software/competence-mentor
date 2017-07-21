using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            return ChartFactory.Get().GetStatistics();
        }


        [Route("api/competence/search/")]
        [HttpGet]
        public string[] Search(string compentence)
        {
            var list = new List<string>();
            var competenceList = compentence.Trim().Split(' ');
            foreach (var competence in competenceList)
            {
                if (string.IsNullOrWhiteSpace(competence))
                    continue;
                list.Add(competence.Trim().ToLower());
            }
            return ChartFactory.Get().Search(list);
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
