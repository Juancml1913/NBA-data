using Newtonsoft.Json;

namespace NBA_data.Common.Models
{
    public class Player
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "height_feet")]
        public string HeightFeet { get; set; }

        [JsonProperty(PropertyName = "height_inches")]
        public string HeightInches { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "position")]
        public string Position { get; set; }

        [JsonProperty(PropertyName = "weight_pounds")]
        public string WeightPounds { get; set; }

        [JsonProperty(PropertyName = "team")]
        public Team Team { get; set; }
    }
}
