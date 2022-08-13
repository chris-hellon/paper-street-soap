using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Interfaces.Services;
using PaperStreetSoap.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Utils;
using ProjectBase.Core.Models;
using PaperStreetSoap.Core.Interfaces.Clients;

namespace PaperStreetSoap.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IErrorLoggerService _errorLoggerService;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IPackagesRepository _packagesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOpenNodeClient _openNodeClient;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IErrorLoggerService errorLoggerService, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ISubscriptionsRepository subscriptionsRepository, IPackagesRepository packagesRepository, IOrdersRepository ordersRepository, IOpenNodeClient openNodeClient)
        {
            _errorLoggerService = errorLoggerService;
            _subscriptionsRepository = subscriptionsRepository;
            _packagesRepository = packagesRepository;
            _ordersRepository = ordersRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _openNodeClient = openNodeClient;
        }

        [Route("/order-confirmation/{orderId}")]
        public async Task<IActionResult> Confirmation(int orderId)
        {
            int? subscriptionId = null;

            try
            {
                var order = await _ordersRepository.GetOrder(orderId);

                if (order != null)
                {
                    var package = await _packagesRepository.GetPackage(order.PackageId);
                    var openNodeInfo = await _openNodeClient.GetChargeInfo(new Core.DTOs.Requests.GetChargeInfoReqest(order.OpenNodeId));

                    if (openNodeInfo.IsSuccessful)
                    {
                        if (openNodeInfo.Data.Data.OrderId == orderId)
                        {
                            string status = openNodeInfo.Data.Data.Status;

                            if (status == "paid")
                            {
                                if (package != null)
                                {
                                    var userId = order.UserId;
                                    var packageId = package.Id;

                                    var startDate = DateTime.Now;
                                    var endDate = DateTime.Now;

                                    switch (package.DurationType)
                                    {
                                        case "Month":
                                            endDate = startDate.AddMonths(package.Duration);
                                            break;
                                        case "Year":
                                            endDate = startDate.AddYears(package.Duration);
                                            break;
                                    }

                                    subscriptionId = await _subscriptionsRepository.CreateSubscription(userId, packageId, orderId);

                                    if (subscriptionId.HasValue)
                                    {
                                        await _ordersRepository.UpdateOrder(orderId, order.OpenNodeId, true);
                                        return View("Confirmation/Index", new Views.Orders.Confirmation.IndexModel(orderId, package.Title, startDate, endDate));
                                    }
                                }
                            }
                            else if (status == "processing") return LocalRedirect($"/order-processing/{orderId}");
                            else return LocalRedirect($"/order-fail/{orderId}");
                        }
                    }
                }

                //if order or subscription cannot be found, or there's an error, delete both
                await Task.WhenAll(DeleteSubscription(subscriptionId), DeleteOrder(orderId));

                return LocalRedirect("/error");
            }
            catch (Exception e)
            {
                //if order or subscription cannot be found, or there's an error, delete both
                await Task.WhenAll(DeleteSubscription(subscriptionId), DeleteOrder(orderId));

                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/confirm-package/{packageId}")]
        public async Task<IActionResult> CreateOpenNodeCharge(int packageId)
        {
            int? orderId = null;

            try
            {
                string userId = Helpers.GetUserId(_httpContextAccessor);

                if (userId != null)
                {
                    var packageRequest = _packagesRepository.GetPackage(packageId);
                    var userRequest = _userManager.FindByIdAsync(userId);

                    await Task.WhenAll(packageRequest, userRequest);

                    if (packageRequest.Result != null && userRequest.Result != null)
                    {
                        var package = packageRequest.Result;
                        var user = userRequest.Result;

                        orderId = await _ordersRepository.CreateOrder(packageId, package.Price, userId);

                        if (orderId.HasValue && orderId.Value > 0)
                        {
                            var successUrl = $"{Request.Scheme}://{Request.Host}/order-confirmation/{orderId.Value}";
                            var createChargeResponse = await _openNodeClient.CreateCharge(new Core.DTOs.Requests.CreateOpenNodeChargeRequest(
                                package.Title,
                                package.Price,
                                orderId.Value,
                                user.UserName,
                                user.Email,
                                user.Email,
                                successUrl));

                            if (createChargeResponse.IsSuccessful)
                            {
                                await _ordersRepository.UpdateOrder(orderId.Value, createChargeResponse.Data.Data.Id);
                                return Redirect(createChargeResponse.Data.Data.CheckoutUrl);
                            }
                        }
                    }
                }

                await DeleteOrder(orderId);
                return LocalRedirect("/error");
            }
            catch (Exception e)
            {
                await DeleteOrder(orderId);

                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/order-processing/{orderId}")]
        public async Task<IActionResult> Processing(int orderId)
        {
            try
            {
                var order = await _ordersRepository.GetOrder(orderId);

                if (order != null)
                {
                    var package = await _packagesRepository.GetPackage(order.PackageId);

                    if (package != null)
                        return View("Processing/Index", new Views.Orders.Processing.IndexModel(orderId, package.Title));
                }

                return LocalRedirect("/error");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/order-fail/{orderId}")]
        public async Task<IActionResult> Fail(int orderId)
        {
            try
            {
                var order = await _ordersRepository.GetOrder(orderId);

                if (order != null)
                {
                    var package = await _packagesRepository.GetPackage(order.PackageId);

                    if (package != null)
                        return View("Fail/Index", new Views.Orders.Fail.IndexModel(order.OpenNodeId, orderId, package.Title));
                }

                return LocalRedirect("/error");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        private async Task DeleteOrder(int? orderId)
        {
            if (orderId.HasValue)
                await _ordersRepository.DeleteOrder(orderId.Value);
        }

        private async Task DeleteSubscription(int? subscriptionId)
        {
            if (subscriptionId.HasValue)
                await _subscriptionsRepository.DeleteSubscription(subscriptionId.Value);
        }
    }
}

