namespace NBA_data.Common.Models
{
    using Newtonsoft.Json;
    using System;
    public class Game
    {
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonIgnore]
        public string DateFormat {
            get {
                return Date.ToString("dd/MM/yyyy");
            }
        }

        [JsonProperty(PropertyName = "home_team")]
        public Team HomeTeam { get; set; }

        [JsonProperty(PropertyName = "home_team_score")]
        public int? HomeTeamScore { get; set; }

        [JsonProperty(PropertyName = "period")]
        public int? Period { get; set; }

        [JsonProperty(PropertyName = "postseason")]
        public bool? PostSeason { get; set; }

        [JsonProperty(PropertyName = "season")]
        public int? Season { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "time")]
        public string Time { get; set; }

        [JsonProperty(PropertyName = "visitor_team")]
        public Team VisitorTeam { get; set; }

        [JsonProperty(PropertyName = "visitor_team_score")]
        public int? VisitorTeamScore { get; set; }
    }
}

