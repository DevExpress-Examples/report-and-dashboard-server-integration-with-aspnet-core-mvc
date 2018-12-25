using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ReportServerIntegration.Services {
    public interface IApiService {
        Task<HttpResponseMessage> GetAsync(string path);
    }

    public class ApiService : IApiService {
        static string GetUri(string path)         {
            return $"api/{path}";
        }

        readonly ITokenService _tokenService;
        readonly IHttpClientFactory _httpClientFactory;

        public ApiService(ITokenService tokenService, IHttpClientFactory httpClientFactory) {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<HttpResponseMessage> GetAsync(string path) {
            string token = await _tokenService.GetToken();

            using(var request = new HttpRequestMessage(HttpMethod.Get, GetUri(path))) {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpClient client = _httpClientFactory.CreateClient("reportServer");

                return await client.SendAsync(request);
            }
        }
    }
}