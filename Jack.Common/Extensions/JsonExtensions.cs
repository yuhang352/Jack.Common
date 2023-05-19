using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text;

namespace YH.Common.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static string SerializeObject(this object obj)
        {
            JsonSerializerSettings settings = DefaultJsonSettings();
            return JsonConvert.SerializeObject(obj, Formatting.None, settings);
        }

        public static T DeserializeObject<T>(this string value)
        {
            JsonSerializerSettings settings = DefaultJsonSettings();
            return JsonConvert.DeserializeObject<T>(value, settings);
        }

        public static byte[] SerializeBytes(this object obj)
        {
            JsonSerializerSettings settings = DefaultJsonSettings();
            string s = JsonConvert.SerializeObject(obj, Formatting.None, settings);
            return Encoding.UTF8.GetBytes(s);
        }

        public static T DeserializeBytes<T>(this byte[] data)
        {
            return Encoding.UTF8.GetString(data).DeserializeObject<T>();
        }

        private static JsonSerializerSettings DefaultJsonSettings()
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            jsonSerializerSettings.Converters.Clear();
            jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            });
            return jsonSerializerSettings;
        }
    }
}
