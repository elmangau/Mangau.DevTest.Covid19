using Mangau.DevTest.Covid19.Dtos;
using Mangau.DevTest.Covid19.Models;
using Mangau.DevTest.Covid19.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            await _reportsModel.FilterTop10(cancellationToken);
            
            ViewBag.Regions = _reportsModel.Regions;
            ViewBag.Statistics = _reportsModel.Statistics;

            return View(new ReportsFormModel() { RegionIso = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Index(ReportsFormModel reports, CancellationToken cancellationToken)
        {
            _reportsModel.RegionIso = reports.RegionIso;

            await _reportsModel.FilterTop10ByIso(cancellationToken);

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
    }
}
