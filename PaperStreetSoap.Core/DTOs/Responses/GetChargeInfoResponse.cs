using Newtonsoft.Json;
namespace PaperStreetSoap.Core.DTOs.Responses
{
    public class GetChargeInfoResponse
    {
        [JsonProperty("data")]
        public GetChargeInfoResponseData Data { get; set; }
    }

    public class GetChargeInfoResponseData
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("order_id")]
        public int OrderId { get; set; }
    }
}

