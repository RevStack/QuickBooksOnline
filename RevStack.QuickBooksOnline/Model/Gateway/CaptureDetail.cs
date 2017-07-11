using Newtonsoft.Json;

namespace RevStack.QuickBooksOnline.Model.Gateway
{
    public class CaptureDetail
    {
        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }
    }
}
