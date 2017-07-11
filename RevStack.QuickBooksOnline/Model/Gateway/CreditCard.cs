using Newtonsoft.Json;

namespace RevStack.QuickBooksOnline.Model.Gateway
{
    public class CreditCard
    {
        [JsonProperty("expYear")]
        public string ExpYear { get; set; }

        [JsonProperty("expMonth")]
        public string ExpMonth { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cvc")]
        public string CVC { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }
    }
}
