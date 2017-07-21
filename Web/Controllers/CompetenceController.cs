using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model.Competence.Domain;
using Model.Competence.Infrastructure;

namespace Web.Controllers
{
    public class CompetenceController : ApiController
    {
        // GET api/competence/{id}
        public string Get(string id)
        {
            return "tekst #kompetencja1";
        }

        
        // POST api/competence
        public void Post(UserCompetenceModel model)
        {
            var repo = new UserCompetenceRepository();
            var entity = repo.Get(model.UserId);
            var command = new CompetenceUpdateCommand {CompentenceText = model.CompetenceText};
            entity.UpdateCompentence(command);
        }

        // PUT api/competence/{id}
        public void Put(string id, [FromBody]string competenceText)
        {
        }

    }

    public class UserCompetenceModel
    {
        public string UserId;
        public string CompetenceText;
    }
}
