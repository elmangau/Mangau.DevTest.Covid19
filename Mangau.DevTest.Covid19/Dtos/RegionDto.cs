using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Mangau.DevTest.Covid19.Dtos
{
    public class RegionDto
    {
        private class RegioDtoComparer : IEqualityComparer<RegionDto>
        {
            public bool Equals(RegionDto x, RegionDto y)
            {
                return StringComparer.InvariantCultureIgnoreCase.Equals(x?.Iso, y?.Iso);
            }

            public int GetHashCode([DisallowNull] RegionDto obj)
            {
                return obj?.Iso.GetHashCode() ?? 0;
            }
        }
        public static IEqualityComparer<RegionDto> EqualityComparer = new RegioDtoComparer();

        public string Iso { get; set; }

        public string Name { get; set; }

        public string Province { get; set; }

        [JsonProperty("lat")]
        public string Latitud { get; set; }

        [JsonProperty("long")]
        public string Longitud { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Province))
            {
                return Name;
            }

            return $"{Name} - {Province}";
        }
    }
}
