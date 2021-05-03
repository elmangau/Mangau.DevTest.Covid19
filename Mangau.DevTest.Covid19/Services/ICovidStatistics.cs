using Mangau.DevTest.Covid19.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mangau.DevTest.Covid19.Services
{
    public interface ICovidStatistics
    {
        Task<ReportsQueryResponseDto> GetReports(ReportsQueryRequestDto request, CancellationToken cancellationToken = default);

        Task<ReportsQueryResponseDto> FilterReportsTop10(CancellationToken cancellationToken = default);

        Task<ReportsQueryResponseDto> FilterReportsTop10ByIso(string regionIso, CancellationToken cancellationToken = default);
    }
}
