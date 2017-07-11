using Newtonsoft.Json;

namespace RevStack.QuickBooksOnline.Model.Gateway
{
    public class Payment : RevStack.Payment.Model.IPayment
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("authCode")]
        public string AuthCode { get; set; }

        [JsonProperty("capture")]
        public string Capture { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("card")]
        public CreditCard Card { get; set; }

        [JsonProperty("context")]
        public Context Context { get; set; }

        [JsonProperty("captureDetail")]
        public CaptureDetail CaptureDetail { get; set; }
        
    }
}
