using Newtonsoft.Json;
using System;

namespace Mangau.DevTest.Covid19.Dtos
{
    public class RegionStatisticsDto
    {
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? Date { get; set; }

        public decimal? Confirmed { get; set; }

        public decimal? Deaths { get; set; }

        public decimal? Recovered { get; set; }

        public decimal? Active { get; set; }

        [JsonProperty("confirmed_diff")]
        public decimal? ConfirmedDifference { get; set; }

        [JsonProperty("deaths_diff")]
        public decimal? DeathsDifference { get; set; }

        [JsonProperty("recovered_diff")]
        public decimal? RecoveredDifference { get; set; }

        [JsonProperty("active_diff")]
        public decimal? ActiveDifference { get; set; }

        [JsonProperty("last_update")]
        [JsonConverter(typeof(JsonDateTimeConverter))]
        public DateTime? LastUpdate { get; set; }

        [JsonProperty("fatality_rate")]
        public decimal? FatalityRate { get; set; }

        public RegionDto Region { get; set; }
    }
}
