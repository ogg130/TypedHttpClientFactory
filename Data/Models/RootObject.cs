using Newtonsoft.Json;

namespace TypedHttpClientFactory.Data.Models
{
     public class RootObject
    {
        [JsonProperty("_base")]
        public string basecurrency { get; set; }
        public Rates rates { get; set; }
        public string date { get; set; }
    }
}
