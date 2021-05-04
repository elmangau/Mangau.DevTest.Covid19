using Mangau.DevTest.Covid19.Dtos;
using Mangau.DevTest.Covid19.Models;
using Mangau.DevTest.Covid19.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mangau.DevTest.Covid19.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReportsModel _reportsModel;

        public HomeController(ILogger<HomeController> logger, ReportsModel reportsModel)
        {
            _logger = logger;
            _reportsModel = reportsModel;
        }

        private async Task Filter(string regionIso, CancellationToken cancellationToken)
        {
            _reportsModel.RegionIso = regionIso;

            if (string.IsNullOrWhiteSpace(regionIso))
            {
                await _reportsModel.FilterTop10(cancellationToken);
            }
            else
            {
                await _reportsModel.FilterTop10ByIso(cancellationToken);
            }

        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            await Filter(null, cancellationToken);
            
            ViewBag.Regions = _reportsModel.Regions;
            ViewBag.Statistics = _reportsModel.Statistics;

            return View(new ReportsFormModel() { RegionIso = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Index(ReportsFormModel reports, CancellationToken cancellationToken)
        {
            await Filter(reports.RegionIso, cancellationToken);

            ViewBag.Regions = _reportsModel.Regions;
            ViewBag.Statistics = _reportsModel.Statistics;

            return View(reports);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> GetXmlFile(string regionIso, CancellationToken cancellationToken)
        {
            await Filter(regionIso, cancellationToken);

            var sb = new StringBuilder();

            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>").AppendLine("<Reports>");

            foreach (var rep in _reportsModel.Statistics)
            {
                sb.AppendLineIndent("<Report>");

                sb.AppendIndent().AppendLineIndent($"<RegionName>{rep.Region}</RegionName>");
                sb.AppendIndent().AppendLineIndent($"<Cases>{rep.Confirmed}</Cases>");
                sb.AppendIndent().AppendLineIndent($"<Deaths>{rep.Deaths}</Deaths>");

                sb.AppendLineIndent("</Report>");
            }

            sb.AppendLine("</Reports>");

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Xml, "COVID-19-Top-10.xml");
        }

        [HttpGet]
        public async Task<IActionResult> GetJsonFile(string regionIso, CancellationToken cancellationToken)
        {
            await Filter(regionIso, cancellationToken);

            var comma = "";
            var sb = new StringBuilder();
            sb.AppendLine("{").AppendIndent("Reports: [");


            foreach (var rep in _reportsModel.Statistics)
            {
                sb.AppendLine(comma).AppendIndent().AppendLineIndent("{");

                sb.AppendIndent().AppendIndent().AppendLineIndent($"RegionName: \"{rep.Region}\",");
                sb.AppendIndent().AppendIndent().AppendLineIndent($"Cases: {rep.Confirmed},");
                sb.AppendIndent().AppendIndent().AppendLineIndent($"Deaths: {rep.Deaths}");

                sb.AppendIndent().AppendIndent("}");

                comma = ",";
            }

            sb.AppendLine().AppendLineIndent("]").AppendLine("}");

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Json, "COVID-19-Top-10.json");
        }

        [HttpGet]
        public async Task<IActionResult> GetCsvFile(string regionIso, CancellationToken cancellationToken)
        {
            await Filter(regionIso, cancellationToken);

            var sb = new StringBuilder();
            sb.AppendLine("RegionName,Cases,Deaths");

            foreach (var rep in _reportsModel.Statistics)
            {
                sb.AppendLine($"\"{rep.Region}\",{rep.Confirmed},{rep.Deaths}");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Json, "COVID-19-Top-10.csv");
        }
    }
}
