using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RevStack.QuickBooksOnline.Model.Gateway
{
    public class Context
    {
        [JsonProperty("tax")]
        public string Tax { get; set; }

        [JsonProperty("recurring")]
        public string Recurring { get; set; }

        [JsonProperty("deviceInfo")]
        public JObject DeviceInfo { get; set; }
    }
}
