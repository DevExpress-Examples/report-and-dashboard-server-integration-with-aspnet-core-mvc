using System.Runtime.Serialization;

namespace DocumentViewerSample.Models {
    [DataContract]
    public class Token {
        [DataMember(Name = "access_token")]
        public string AuthToken { get; set; }
    }
}
