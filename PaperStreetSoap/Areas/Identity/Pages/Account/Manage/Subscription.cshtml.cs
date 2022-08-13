using PaperStreetSoap.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaperStreetSoap.Core.Models;
using System.Security.Claims;
using PaperStreetSoap.Core.Interfaces.Clients;

namespace PaperStreetSoap.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class SubscriptionModel : PageModel
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IPackagesRepository _packagesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOpenNodeClient _openNodeClient;

        public Subscription Subscription { get; set; } = null;
        public Order UnpaidOrder { get; set; } = null;
        public Package UnpaidPackage { get; set; } = null;

        public SubscriptionModel(ISubscriptionsRepository subscriptionsRepository, IOrdersRepository ordersRepository, IHttpContextAccessor httpContextAccessor, IOpenNodeClient openNodeClient, IPackagesRepository packagesRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _httpContextAccessor = httpContextAccessor;
            _ordersRepository = ordersRepository;
            _openNodeClient = openNodeClient;
            _packagesRepository = packagesRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                Subscription = await _subscriptionsRepository.GetSubscription(userId);

                if (Subscription == null)
                {
                    var outstandingOrder = await _ordersRepository.GetUsersOutstandingOrder(userId);

                    if (outstandingOrder != null)
                    {
                        var openNodeInfo = await _openNodeClient.GetChargeInfo(new Core.DTOs.Requests.GetChargeInfoReqest(outstandingOrder.OpenNodeId));

                        if (openNodeInfo.IsSuccessful && openNodeInfo.Data != null)
                        {
                            if (openNodeInfo.Data.Data != null)
                            {
                                string status = openNodeInfo.Data.Data.Status;

                                if (status == "pending")
                                    return LocalRedirect($"/order-processing/{outstandingOrder.Id}");
                                else if (status == "paid")
                                    return LocalRedirect($"/order-confirmation/{outstandingOrder.Id}");
                                else if (status == "unpaid")
                                {
                                    UnpaidOrder = outstandingOrder;
                                    UnpaidPackage = await _packagesRepository.GetPackage(outstandingOrder.PackageId);
                                }
                                    
                            }
                        }
                    }
                }
            }

            return Page();
        }
    }
}
