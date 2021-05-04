using Mangau.DevTest.Covid19.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangau.DevTest.Covid19
{
    public static class Extensions
    {
        public static string ToQueryString(this ReportsQueryRequestDto request)
        {
            var prms = new List<string>(5);

            if (request.Date.HasValue)
            {
                prms.Add($"date={request.Date.Value.ToString("yyyy-MM-dd")}");
            }

            if (!string.IsNullOrWhiteSpace(request.RegionIso))
            {
                prms.Add($"iso={request.RegionIso}");
            }

            if (!string.IsNullOrWhiteSpace(request.RegionProvince))
            {
                prms.Add($"region_province={request.RegionProvince}");
            }

            if (!string.IsNullOrWhiteSpace(request.CityName))
            {
                prms.Add($"city_name={request.CityName}");
            }

            return string.Join("&", prms);
        }

        public static StringBuilder AppendIndent(this StringBuilder stringBuilder, string text = "")
        {
            return stringBuilder.Append($"    {text}");
        }

        public static StringBuilder AppendLineIndent(this StringBuilder stringBuilder, string text = "")
        {
            return stringBuilder.AppendLine($"    {text}");
        }
    }
}
