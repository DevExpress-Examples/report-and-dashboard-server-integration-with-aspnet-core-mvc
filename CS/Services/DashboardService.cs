using System;
using System.Threading.Tasks;
using ReportServerIntegration.Models;
using Microsoft.Extensions.Configuration;

namespace ReportServerIntegration.Services {
    public interface IDashboardService {
        Task<DashboardViewerModel> GetViewerModel(string dashboardId);
    }

    public class DashboardService : IDashboardService {
        readonly ITokenService _tokenService;
        readonly IConfiguration _configuration;

        public DashboardService(ITokenService tokenService, IConfiguration configuration) {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<DashboardViewerModel> GetViewerModel(string dashboardId) {
            string token = await _tokenService.GetToken();
            return new DashboardViewerModel {
                DesignerUri = $"{_configuration["ReportServerBaseUri"]}/dashboardDesigner",
                DashboardId = dashboardId,
                AuthToken = token
            };
        }
    }
}