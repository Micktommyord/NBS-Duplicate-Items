using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NBS_Duplicate_Items
{
    public class ItemsModel
    {
        [JsonPropertyName("app")]
        [MaxLength(32)]
        public string ApplicationId { get; set; }

        [JsonPropertyName("action")]
        [MaxLength(32)]
        public string Action { get; set; }

        [JsonPropertyName("params")]
        [MaxLength(16)]
        public Dictionary<string, object> Parameters { get; set; }

        public override string ToString()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,
                AllowTrailingCommas = true,
                IgnoreNullValues = true,
                WriteIndented = true,
                
            };
            return JsonSerializer.Serialize(this, options);
        }
    }
}
