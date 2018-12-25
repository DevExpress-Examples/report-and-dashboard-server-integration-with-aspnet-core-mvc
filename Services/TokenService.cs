using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ReportServerIntegration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ReportServerIntegration.Services {
    public interface ITokenService {
        Task<string> GetToken();        
    }

    public class TokenService : ITokenService {
        const string TokenCookie = "ReportServerToken";

        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IHttpClientFactory _httpClientFactory;
        readonly IConfiguration _configuration;

        HttpRequest Request => _httpContextAccessor.HttpContext.Request;
        HttpResponse Response => _httpContextAccessor.HttpContext.Response;

        public TokenService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IConfiguration configuration) {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<string> GetToken() {
            string token = Request.Cookies[TokenCookie];

            if(string.IsNullOrEmpty(token)) {
                token = await RequestToken();

                Response.Cookies.Append(TokenCookie, token, new CookieOptions {
                    HttpOnly = true,
                    IsEssential = true                    
                });
            }
            return token;
        }

        async Task<string> RequestToken() {
            string username = _configuration["ReportServer:UserName"];
            string password = _configuration["ReportServer:UserPassword"];

            var requestContent = new Dictionary<string, string> {
                { "grant_type", "password"},
                { "username", username },
                { "password", password }
            };

            HttpClient client = _httpClientFactory.CreateClient("reportServer");
            HttpResponseMessage response = await client.PostAsync("oauth/token", new FormUrlEncodedContent(requestContent));            
            Token token = await response.EnsureSuccessStatusCode().Content.ReadAsAsync<Token>();
            return token.AuthToken;
        }        
    }
}