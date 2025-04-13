using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLeagueReplayParser.Serializers
{    public class ClassNetCacheJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ClassNetCache);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            ClassNetCache classNetCache = (ClassNetCache)value;

            writer.WriteStartObject();

            writer.WriteKeyValue("ObjectIndex", classNetCache.ObjectIndex, serializer);
            writer.WriteKeyValue("ParentId", classNetCache.ParentId, serializer);
            writer.WriteKeyValue("Id", classNetCache.Id, serializer);
            writer.WriteKeyValue("Properties", classNetCache.Properties.Values, serializer);

            writer.WriteEndObject();
        }
    }
}
