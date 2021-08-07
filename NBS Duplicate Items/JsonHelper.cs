using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NBS_Duplicate_Items
{
    public static class JsonHelper
    {
        public static List<string> NormalizeStringAndFlatten(string json)
        {
            JArray parsedObjects = JArray.Parse(json);
            JArray normalizedObjects = new JArray();

            foreach (JObject jObject in parsedObjects)
            {
                JObject normalizedObject = SortPropertiesAlphabetically(jObject);
                normalizedObjects.Add(normalizedObject);
            }

            var reSerialized = JsonConvert.SerializeObject(normalizedObjects);

            return FlattenJsonToListString(reSerialized);
        }

        private static JObject SortPropertiesAlphabetically(JObject original)
        {
            JObject result = new JObject();

            foreach (var property in original.Properties().ToList().OrderBy(p => p.Name))
            {
                if (property.Value is JObject value)
                {
                    value = SortPropertiesAlphabetically(value);
                    result.Add(property.Name, value);
                }
                else
                {
                    result.Add(property.Name, property.Value);
                }
            }

            return result;
        }

        public static List<string> FlattenJsonToListString(string json)
        {
            var listJson = JArray.Parse(json);
                return listJson
                .Select(j => j.ToString())
                .ToList();
        }
    }
}
