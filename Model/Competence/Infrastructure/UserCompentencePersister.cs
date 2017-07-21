using System;
using System.IO;
using Model.Competence.Domain;
using Newtonsoft.Json;

namespace Model.Competence.Infrastructure
{
    public class UserCompentencePersister : IUserCompentencePersister
    {
        private static readonly object SyncRoot = new object();

        public void Store(UserCompetence aggregate, CompetenceUpdateCommand command)
        {
            var aggregateId = aggregate.GetId();
            var envelope = new CommandEnvelope(aggregateId, command);

            var json = JsonConvert.SerializeObject(envelope);
            var o = JsonConvert.DeserializeObject<CommandEnvelope>(json);
            //var c = ((JObject) o.Command).ToObject(typeof(CompentenceUpdateCommand));

            lock (SyncRoot)
            {
                File.AppendAllLines("c:\\temp\\store.db", new[] {json});
            }
        }
    }

    public class CommandEnvelope
    {
        [JsonProperty(PropertyName = "stream-id")] public string StreamId;

        [JsonProperty(PropertyName = "created")] public DateTime Created;

        [JsonProperty(PropertyName = "command-type")] public string CommandType;

        [JsonProperty(PropertyName = "command")] public object Command;

        public CommandEnvelope(string streamId, object command)
        {
            StreamId = streamId;
            Created = DateTime.Now;
            Command = command;
            CommandType = command.GetType().FullName;
        }
    }
}