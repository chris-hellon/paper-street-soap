using Newtonsoft.Json;
namespace PaperStreetSoap.Core.DTOs.Requests
{
    public class GetChargeInfoReqest
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public GetChargeInfoReqest(string id)
        {
            Id = id;
        }
    }
}

