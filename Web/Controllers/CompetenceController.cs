using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class CompetenceController : ApiController
    {
        // GET api/competence/{userId}
        public string Get(string userId)
        {
            return "tekst #kompetencja1";
        }

        // POST api/competence
        public void Post(string id, [FromBody]string value)
        {
        }

        // PUT api/competence/{id}
        public void Put(UserCompetenceModel model)
        {
        }
    }

    public class UserCompetenceModel
    {
        public string UserId;
        public string CompentenceText;
    }
}
