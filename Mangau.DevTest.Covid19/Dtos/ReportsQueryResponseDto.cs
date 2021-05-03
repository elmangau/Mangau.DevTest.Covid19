using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Mangau.DevTest.Covid19.Dtos
{
    public class ReportsQueryResponseDto
    {
        private List<RegionStatisticsDto> _regions = new List<RegionStatisticsDto>();

        [JsonIgnore]
        public List<RegionStatisticsDto> Regions
        {
            get => _regions;
            set
            {
                Data = value;
            }
        }

        public IEnumerable<RegionStatisticsDto> Data
        {
            get => _regions;
            set
            {
                _regions.Clear();

                if (value != null && value.Any())
                {
                    _regions.AddRange(value);
                }
            }
        }
    }
}
