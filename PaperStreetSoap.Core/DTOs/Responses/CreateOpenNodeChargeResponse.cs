using Newtonsoft.Json;

namespace PaperStreetSoap.Core.DTOs.Responses
{
    public class CreateOpenNodeChargeResponse
    {
        [JsonProperty("data")]
        public CreateOpenNodeChargeResponseData Data { get; set; }
    }

    public class CreateOpenNodeChargeResponseData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("success_url")]
        public string SuccessUrl { get; set; }

        [JsonProperty("hosted_checkout_url")]
        public string CheckoutUrl { get; set; }

        [JsonProperty("order_id")]
        public int OrderId { get; set; }
    }
}

