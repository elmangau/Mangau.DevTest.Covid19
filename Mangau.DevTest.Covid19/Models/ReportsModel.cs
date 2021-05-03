using Mangau.DevTest.Covid19.Dtos;
using Mangau.DevTest.Covid19.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mangau.DevTest.Covid19.Models
{
    public class ReportsModel
    {
        private readonly ICovidStatistics _covidStatistics;
        private IEnumerable<RegionDto> _regions = new RegionDto[0];
        private IEnumerable<RegionStatisticsDto> _statistics = new RegionStatisticsDto[0];

        public ReportsModel(ICovidStatistics covidStatistics)
        {
            _covidStatistics = covidStatistics;
        }

        public string RegionIso { get; set; }

        public IEnumerable<RegionDto> Regions => _regions;

        public IEnumerable<RegionStatisticsDto> Statistics => _statistics;

        public async Task FilterTop10(CancellationToken cancellationToken = default)
        {
            var response = await _covidStatistics.FilterReportsTop10(cancellationToken);

            _regions = response.Data
                .Select(r => r.Region)
                .Distinct(RegionDto.EqualityComparer);

            _statistics = response.Regions;
        }

        public async Task FilterTop10ByIso(CancellationToken cancellationToken = default)
        {
            var response = await _covidStatistics.FilterReportsTop10ByIso(RegionIso, cancellationToken);

            _regions = response.Data
                .Select(r => r.Region)
                .Distinct(RegionDto.EqualityComparer);

            _statistics = response.Regions;
        }


    }
}
