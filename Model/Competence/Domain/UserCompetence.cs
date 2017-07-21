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
        private readonly State state;

        public UserCompetence(string userId, IUserCompentencePersister userCompentencePersister)
        {
            this.userCompentencePersister = userCompentencePersister;
            state = new State(userId);
        }

        public string GetId()
        {
            return this.state.Id;
        }

        public void UpdateCompentence(CompetenceUpdateCommand command)
        {
            var parser = new UserCompetenceParser();
            command.Competencies = parser.ParseCompetenceText(command.CompentenceText);
            state.Apply(command);
            userCompentencePersister.Store(this, command);
        }

        internal class State
        {
            public readonly string Id;
            public string compentenceText;

            public State(string userId)
            {
                this.Id = userId;
                compentenceText = "";
            }

            public void Apply(CompetenceUpdateCommand command)
            {
                compentenceText = command.CompentenceText;
            }
        }
    }

    public class CompetenceUpdateCommand
    {
        public string CompentenceText { get; set; }
        public string[] Competencies { get; set; }
    }
}
