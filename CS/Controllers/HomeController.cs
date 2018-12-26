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
        public async Task<IActionResult> Index([FromServices] IApiService api) {
            HttpResponseMessage response = await api.GetAsync("documents");
            string responseString = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            var documents =  JsonConvert.DeserializeObject<DocumentModel[]>(responseString);
            return View(documents);
        }

        [HttpGet]
        [Route("report/{reportId}")]
        public async Task<IActionResult> ReportViewer(string reportId, [FromServices] IReportService reportService) {
            ReportViewerModel model = await reportService.GetViewerModel(reportId);
            return View(model);
        }

        [HttpGet]
        [Route("dashboard/{dashboardId}")]
        public async Task<IActionResult> DashboardViewer(string dashboardId, [FromServices] IDashboardService dashboardService) {
            DashboardViewerModel model = await dashboardService.GetViewerModel(dashboardId);
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
