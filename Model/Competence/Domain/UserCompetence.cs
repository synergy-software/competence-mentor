using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Competence.Domain
{
    public class UserCompetence
    {
        private readonly IUserCompentencePersister userCompentencePersister;
        private State state;

        public UserCompetence(string userId, IUserCompentencePersister userCompentencePersister)
        {
            this.userCompentencePersister = userCompentencePersister;
            state = new State(userId);
        }

        public void UpdateCompentence(CompentenceUpdateCommand command)
        {
            // todo parse
            state.Apply(command);
            userCompentencePersister.Store(command);
        }

        public string GetCompetenceText()
        {
            return state.compentenceText;
        }

        class State
        {
            public string userId;
            public string compentenceText;

            public State(string userId)
            {
                userId = this.userId;
                compentenceText = "";
            }

            public void Apply(CompentenceUpdateCommand command)
            {
                compentenceText = command.CompentenceText;
            }
        }
    }

    public class CompentenceUpdateCommand
    {
        public string CompentenceText { get; set; }
    }
}
