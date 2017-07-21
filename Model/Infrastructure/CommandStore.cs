using System;
using System.IO;
using Model.Competence.Domain;
using Newtonsoft.Json;

namespace Model.Infrastructure
{
    public static class CommandStore
    {
        private static readonly object SyncRoot = new object();
        public static string DatabasePath { get; set; }

        public static void Store(UserCompetence aggregate, CompetenceUpdateCommand command)
        {
            var aggregateId = aggregate.GetId();
            var envelope = new CommandEnvelope(aggregateId, command);

            var json = JsonConvert.SerializeObject(envelope);
            var o = JsonConvert.DeserializeObject<CommandEnvelope>(json);
            //var c = ((JObject) o.Command).ToObject(typeof(CompentenceUpdateCommand));

            lock (SyncRoot)
            {
                var storageFile = Path.Combine(DatabasePath, "store.db");
                File.AppendAllLines(storageFile, new[] {json});
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