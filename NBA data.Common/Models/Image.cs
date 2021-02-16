namespace NBA_data.Common.Models
{
    using Newtonsoft.Json;
    public class Image
    {
        [JsonProperty(PropertyName = "value")]
        public Value [] Value { get; set; }
    }

    public class Value {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
