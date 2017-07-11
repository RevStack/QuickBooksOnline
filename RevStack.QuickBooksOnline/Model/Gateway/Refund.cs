using Newtonsoft.Json;

namespace RevStack.QuickBooksOnline.Model.Gateway
{
    public class Refund : RevStack.Payment.Model.IPayment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        
        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
