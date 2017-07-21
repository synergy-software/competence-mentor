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


        [Route("api/statistics")]
        [HttpGet]
        public CompetenceSummary[] Statistics()
        {
            var entity = new ChartManager();
            return entity.GetStatistics();
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
