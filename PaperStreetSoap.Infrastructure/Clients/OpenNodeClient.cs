using RestSharp;
using Microsoft.AspNetCore.Http;
using ProjectBase.Core.Interfaces.Services;
using ProjectBase.Infrastructure.Clients;
using PaperStreetSoap.Core.Interfaces.Clients;
using PaperStreetSoap.Core.DTOs.Requests;
using PaperStreetSoap.Core.DTOs.Responses;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace PaperStreetSoap.Infrastructure.Clients
{
    public class OpenNodeClient : BaseApiClient, IOpenNodeClient
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        public OpenNodeClient(IConfiguration configuration, IErrorLoggerService errorLoggerService, IHttpContextAccessor httpContextAccessor) : base(errorLoggerService, httpContextAccessor, "https://api.opennode.com/v1")
        {
            _configuration = configuration;
            _apiKey = _configuration["OpenNodeApiKey"];
        }

        public async Task<RestResponse<CreateOpenNodeChargeResponse>> CreateCharge(CreateOpenNodeChargeRequest request)
        {
            RestRequest restRequest = new("/charges", Method.Post);
            restRequest.AddHeader("Authorization", _apiKey);
            restRequest.AddHeader("Content-Type", "application/json");

            var serializedBody = JsonConvert.SerializeObject(request);
            restRequest.AddParameter("application/json", serializedBody, ParameterType.RequestBody);

            return await PostAsync<CreateOpenNodeChargeResponse>(restRequest);
        }

        public async Task<RestResponse<GetChargeInfoResponse>> GetChargeInfo(GetChargeInfoReqest reqest)
        {
            RestRequest restRequest = new($"/charge/{reqest.Id}");
            restRequest.AddHeader("Authorization", _apiKey);
            restRequest.AddHeader("Content-Type", "application/json");

            return await GetAsync<GetChargeInfoResponse>(restRequest);
        }
    }
}
