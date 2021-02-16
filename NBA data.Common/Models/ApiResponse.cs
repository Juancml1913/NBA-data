namespace NBA_data.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    public class ApiResponse<T>
    {
        [JsonProperty(PropertyName = "data")]
        public List<T> Data  { get; set; }

        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }
    }
}
