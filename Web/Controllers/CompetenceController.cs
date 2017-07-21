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
        public UserCompetenceGetModel Get(string id)
        {
            return new UserCompetenceGetModel
            {
                CompetenceText = "tekst #kompetencja1 #kompetencja2",
                Competencies = new[] {"#kompetencja1 #kompetencja2"}
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
