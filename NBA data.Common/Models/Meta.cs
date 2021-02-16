namespace NBA_data.Common.Models
{
    using Newtonsoft.Json;
    public class Meta
    {
        [JsonProperty(PropertyName = "total_pages")]
        public int? TotalPages { get; set; }

        [JsonProperty(PropertyName = "current_page")]
        public int? CurrentPage { get; set; }

        [JsonProperty(PropertyName = "next_page")]
        public int? NextPage { get; set; }

        [JsonProperty(PropertyName = "per_page")]
        public int? PerPage { get; set; }

        [JsonProperty(PropertyName = "total_count")]
        public int? TotalCount { get; set; }
    }
}
