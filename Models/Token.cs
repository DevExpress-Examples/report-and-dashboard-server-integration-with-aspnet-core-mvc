using System.Runtime.Serialization;

namespace ReportServerIntegration.Models {
    [DataContract]
    public class Token {
        [DataMember(Name = "access_token")]
        public string AuthToken { get; set; }
    }
}
