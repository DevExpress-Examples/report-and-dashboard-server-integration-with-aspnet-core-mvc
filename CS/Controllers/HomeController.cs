using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportServerIntegration.Models;
using Microsoft.Extensions.Configuration;
using ReportServerIntegration.Services;
using System.Net.Http;
using Newtonsoft.Json;

namespace ReportServerIntegration.Controllers {
    public class HomeController : Controller {
        readonly IApiService _api;
        readonly IReportService _reportService;
        readonly IDashboardService _dashboardService;

        public HomeController(IApiService api, IReportService reportService, IDashboardService dashboardService) {
            _api = api ?? throw new ArgumentNullException(nameof(api));
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _dashboardService = dashboardService ?? throw new ArgumentNullException(nameof(dashboardService));
        }

        public async Task<IActionResult> Index() {
            HttpResponseMessage response = await _api.GetAsync("documents");
            string responseString = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            var documents =  JsonConvert.DeserializeObject<DocumentModel[]>(responseString);
            return View(documents);
        }

        [HttpGet]
        [Route("report/{reportId}")]
        public async Task<IActionResult> ReportViewer(string reportId) {
            ReportViewerModel model = await _reportService.GetViewerModel(reportId);
            return View(model);
        }

        [HttpGet]
        [Route("dashboard/{dashboardId}")]
        public async Task<IActionResult> DashboardViewer(string dashboardId) {
            DashboardViewerModel model = await _dashboardService.GetViewerModel(dashboardId);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
