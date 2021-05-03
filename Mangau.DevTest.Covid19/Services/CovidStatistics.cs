using Mangau.DevTest.Covid19.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Mangau.DevTest.Covid19.Services
{
    public class CovidStatistics : ICovidStatistics
    {
        private readonly HttpClient _client;

        public CovidStatistics(HttpClient client)
        {
            _client = client;
        }

        public async Task<ReportsQueryResponseDto> GetReports(ReportsQueryRequestDto request, CancellationToken cancellationToken = default)
        {

            var response = await _client.GetAsync($"?{request.ToQueryString()}", HttpCompletionOption.ResponseContentRead, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ReportsQueryResponseDto>(cancellationToken: cancellationToken);
        }

        public async Task<ReportsQueryResponseDto> FilterReportsTop10(CancellationToken cancellationToken = default)
        {
            var request = new ReportsQueryRequestDto();

            var response = await GetReports(request, cancellationToken);

            var filtered = response.Regions
                .OrderByDescending(r => r.Confirmed)
                .Take(10)
                .ToList();

            response.Data = filtered;

            return response;
        }

        public async Task<ReportsQueryResponseDto> FilterReportsTop10ByIso(string regionIso, CancellationToken cancellationToken = default)
        {
            var request = new ReportsQueryRequestDto() { RegionIso = regionIso };

            var response = await GetReports(request, cancellationToken);

            var filtered = response.Regions
                .OrderByDescending(r => r.Confirmed)
                .Take(10)
                .ToList();

            response.Data = filtered;

            return response;
        }
    }
}
