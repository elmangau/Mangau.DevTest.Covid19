using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mangau.DevTest.Covid19.Dtos
{
    public class ReportsQueryRequestDto
    {
        public DateTime? Date { get; set; }

        [JsonProperty("iso")]
        public string RegionIso { get; set; }

        [JsonProperty("region_province")]
        public string RegionProvince { get; set; }

        [JsonProperty("city_name")]
        public string CityName { get; set; }
    }
}
