using Newtonsoft.Json;

namespace ReportServerIntegration.Models {
    public class Token {
        [JsonProperty("access_token")]
        public string AuthToken { get; set; }
    }
}
