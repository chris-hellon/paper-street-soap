using ProjectBase.Core.DTOs;
using RestSharp;
using PaperStreetSoap.Core.DTOs.Requests;
using PaperStreetSoap.Core.DTOs.Responses;

namespace PaperStreetSoap.Core.Interfaces.Clients
{
    public interface IOpenNodeClient
    {
        Task<RestResponse<CreateOpenNodeChargeResponse>> CreateCharge(CreateOpenNodeChargeRequest request);

        Task<RestResponse<GetChargeInfoResponse>> GetChargeInfo(GetChargeInfoReqest reqest);
    }
}

