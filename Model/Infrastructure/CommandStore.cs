using System;
using System.Collections.Generic;
using System.IO;
using Model.Competence.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Model.Infrastructure
{
    // TODO: Przyspiesz wydajność zapisując w pamięci kopertki a plik jest tylko backup storem (wykorzystywanym przy podniesieniu aplikacji)

    public static class CommandStore
    {
        private static readonly object SyncRoot = new object();
        public static string DatabasePath { get; set; }
        private static string DatabaseFilePath => Path.Combine(DatabasePath, "store.db");

        public static void AppendToStream(string streamId, CompetenceUpdateCommand command, string commandType)
        {
            var envelope = new CommandEnvelope(streamId, command, commandType);
            var json = JsonConvert.SerializeObject(envelope);

            lock (SyncRoot)
            {
                var storageFile = DatabaseFilePath;
                File.AppendAllLines(storageFile, new[] {json});
            }
        }

        public static IEnumerable<CommandEnvelope> ReadAll()
        {
            string[] lines;
            lock (SyncRoot)
            {
                try
                {
                    lines = File.ReadAllLines(DatabaseFilePath);
                }
                catch (FileNotFoundException)
                {
                    yield break;
                }
            }

            foreach (var line in lines)
            {
                var envelope = JsonConvert.DeserializeObject<CommandEnvelope>(line);
                yield return envelope;
                //var c = ;
            }
        }

        public static IEnumerable<CommandEnvelope> ReadFromStream(string streamId)
        {
            foreach (var envelope in ReadAll())
            {
                if (envelope.StreamId == streamId)
                    yield return envelope;
            }
        }
    }

    public class CommandEnvelope
    {
        [JsonProperty(PropertyName = "stream-id")] public string StreamId;

        [JsonProperty(PropertyName = "created")] public DateTime Created;

        [JsonProperty(PropertyName = "command-type")] public string CommandType;

        [JsonProperty(PropertyName = "command")] public object Command;

        public CommandEnvelope(string streamId, object command, string commandType)
        {
            StreamId = streamId;
            Created = DateTime.Now;
            Command = command;
            CommandType = commandType;
        }

        public T GetCommand<T>()
        {
            var jObject = (JObject) Command;
            return (T) jObject.ToObject(typeof(T));
        }
    }
}