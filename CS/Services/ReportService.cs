using System;
using System.Threading.Tasks;
using ReportServerIntegration.Models;
using Microsoft.Extensions.Configuration;

namespace ReportServerIntegration.Services {
    public interface IReportService {
        Task<ReportViewerModel> GetViewerModel(string reportId);
    }

    public class ReportService : IReportService {
        readonly ITokenService _tokenService;
        readonly IConfiguration _configuration;

        public ReportService(ITokenService tokenService, IConfiguration configuration) {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<ReportViewerModel> GetViewerModel(string reportId) {
            string token = await _tokenService.GetToken();
            return new ReportViewerModel {
                ServerUri = _configuration["ReportServerBaseUri"],
                ReportUri = $"report/{reportId}",                
                AuthToken = token
            };
        }
    }
}