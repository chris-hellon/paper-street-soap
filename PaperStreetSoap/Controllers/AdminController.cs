using ProjectBase.Core.Interfaces.Repositories;
using ProjectBase.Core.Interfaces.Services;
using PaperStreetSoap.Core.Interfaces.Services;
using PaperStreetSoap.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using PaperStreetSoap.Views.Admin.Partials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Models;
using ProjectBase.Core.Utils;

namespace PaperStreetSoap.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IErrorLoggerService _errorLoggerService;
        private readonly IHomeRepository _homeRepository;
        private readonly IChartsRepository _chartsRepository;
        private readonly IPackagesRepository _packagesRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IVideosRepository _videosRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IAzureStorageRepository _azureStorageRepository;
        private readonly IPageContentRepository _pageContentRepository;
        private readonly IEmailService _emailService;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IErrorLoggerService errorLoggerService, IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager, IHomeRepository homeRepository, IAdminRepository adminRepository, IVideosRepository videosRepository, IAzureStorageRepository azureStorageRepository, ISubscriptionsRepository subscriptionsRepository, IOrdersRepository ordersRepository, IChartsRepository chartsRepository, IPackagesRepository packagesRepository, IPageContentRepository pageContentRepository, IUsersRepository usersRepository, IEmailService emailService)
        {
            _errorLoggerService = errorLoggerService;
            _homeRepository = homeRepository;
            _adminRepository = adminRepository;
            _videosRepository = videosRepository;
            _azureStorageRepository = azureStorageRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _packagesRepository = packagesRepository;
            _chartsRepository = chartsRepository;
            _ordersRepository = ordersRepository;
            _pageContentRepository = pageContentRepository;
            _usersRepository = usersRepository;
            _emailService = emailService;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
        }

        #region GET
        [Route("/admin/charts")]
        public async Task<IActionResult> Charts()
        {
            try
            {
                var charts = await _chartsRepository.GetCharts();

                return View("Charts/Index", new Views.Admin.Charts.IndexModel(charts));
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/admin/packages")]
        public async Task<IActionResult> Packages()
        {
            try
            {
                var packages = await _packagesRepository.GetPackages();

                return View("Packages/Index", new Views.Admin.Packages.IndexModel(packages));
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/admin/videos")]
        public async Task<IActionResult> Videos()
        {
            try
            {
                var videos = await _videosRepository.GetAllVideos(true); ;

                return View("Videos/Index", new Views.Admin.Videos.IndexModel(videos));
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/admin/live-trading")]
        public async Task<IActionResult> LiveTrading()
        {
            try
            {
                var charts = await _chartsRepository.GetLiveTradingCharts();

                return View("LiveTrading/Index", new Views.Admin.Charts.IndexModel(charts));
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/admin/users")]
        public async Task<IActionResult> Users()
        {
            try
            {
                var users = await _adminRepository.GetUsers();

                return View("Users/Index", new Views.Admin.Users.IndexModel(users));
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e); 
                return LocalRedirect("/error");
            }
        }

        [Route("/admin/orders")]
        public async Task<IActionResult> Orders()
        {
            try
            {
                var orders = await _ordersRepository.GetOrders();

                return View("Orders/Index", new Views.Admin.Orders.IndexModel(orders));
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/admin/pages/home")]
        public async Task<IActionResult> HomePage()
        {
            try
            {
                var pageSections = await _pageContentRepository.GetPageSections(1);

                if (pageSections.Any())
                {
                    foreach (var pageSection in pageSections)
                    {
                        var content = await _pageContentRepository.GetPageSectionContent(pageSection.Id);

                        if (content.Any())
                            pageSection.Contents = content.ToList();
                    }
                }

                return View("/Views/Admin/Pages/Home/Index.cshtml", new Views.Admin.Pages.Home.IndexModel(pageSections));
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/admin/pages/about")]
        public async Task<IActionResult> About()
        {
            try
            {
                var pageSections = await _pageContentRepository.GetPageSections(2);
                Core.Models.PageContent pageContent = null;

                if (pageSections.Any())
                {
                    var pageSection = pageSections.First();

                    var content = await _pageContentRepository.GetPageSectionContent(pageSection.Id);

                    if (!content.Any())
                    {
                        await _pageContentRepository.CreatePageSectionContent(pageSection.Id, "", "About");
                        content = await _pageContentRepository.GetPageSectionContent(pageSection.Id);
                    }

                    pageContent = content.First();
                }

                return View("/Views/Admin/Pages/About/Index.cshtml", pageContent);
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/admin/pages/terms-and-conditions")]
        public async Task<IActionResult> TermsAndConditions()
        {
            try
            {
                var pageSections = await _pageContentRepository.GetPageSections(3);
                Core.Models.PageContent pageContent = null;

                if (pageSections.Any())
                {
                    var pageSection = pageSections.First();

                    var content = await _pageContentRepository.GetPageSectionContent(pageSection.Id);

                    if (!content.Any())
                    {
                        await _pageContentRepository.CreatePageSectionContent(pageSection.Id, "", "Terms & Conditions");
                        content = await _pageContentRepository.GetPageSectionContent(pageSection.Id);
                    }

                    pageContent = content.First();
                }

                return View("/Views/Admin/Pages/TermsAndConditions/Index.cshtml", pageContent);
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/admin/pages/disclaimer")]
        public async Task<IActionResult> Disclaimer()
        {
            try
            {
                var pageSections = await _pageContentRepository.GetPageSections(4);
                Core.Models.PageContent pageContent = null;

                if (pageSections.Any())
                {
                    var pageSection = pageSections.First();

                    var content = await _pageContentRepository.GetPageSectionContent(pageSection.Id);

                    if (!content.Any())
                    {
                        await _pageContentRepository.CreatePageSectionContent(pageSection.Id, "", "Disclaimer");
                        content = await _pageContentRepository.GetPageSectionContent(pageSection.Id);
                    }

                    pageContent = content.First();
                }

                return View("/Views/Admin/Pages/TermsAndConditions/Index.cshtml", pageContent);
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }
        #endregion

        #region POST
        public async Task<IActionResult> CreateChart(AddChartFormModel model)
        {
            try
            {
                if (model.Title != null)
                {
                    if (model.UploadedFile != null)
                        model.ImageUrl = await _azureStorageRepository.UploadImage(model.UploadedFile, "charts", model.Title);

                    if (model.ImageUrl != null)
                        await _adminRepository.CreateChart(model.Title, model.ImageUrl, false);
                }

                return LocalRedirect("/admin/charts");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> CreateLiveTrading(AddChartFormModel model)
        {
            try
            {
                if (model.Title != null)
                {
                    if (model.UploadedFile != null)
                        model.ImageUrl = await _azureStorageRepository.UploadImage(model.UploadedFile, "charts", model.Title);

                    if (model.ImageUrl != null)
                        await _adminRepository.CreateChart(model.Title, model.ImageUrl, true);
                }

                return LocalRedirect("/admin/live-trading");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> CreatePackage(AddPackageFormModel model)
        {
            try
            {
                if (model.Price.HasValue && model.Title != null)
                    await _adminRepository.CreatePackage(model.Title, model.Price.Value, model.Duration, model.DurationType, model.Description);

                return LocalRedirect("/admin/packages");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> CreateVideo(AddVideoFormModel model)
        {
            try
            {
                if (model.VideoUrl != null && model.Title != null && model.Details != null)
                {
                    int videoId = await _adminRepository.CreateVideo(model.VideoUrl, model.Title, model.Details, model.MarketReview, model.Resources, model.MembersEducation, model.TodaysReview, model.FreeGmBitcoin, model.FreeEducation);

                    if (videoId > 0)
                    {
                        var emailSubscribedUsers = await _usersRepository.GetEmailSubscribedUsers();

                        if (emailSubscribedUsers.Any())
                        {
                            string membersCategory = null;
                            string freeContentCategory = null;

                            if (model.MarketReview)
                                membersCategory = "market-reviews";
                            else if (model.Resources)
                                membersCategory = "resources";
                            else if (model.MembersEducation)
                                membersCategory = "education";
                            else if (model.TodaysReview)
                                membersCategory = "todays-review";
                            else if (model.FreeEducation)
                                freeContentCategory = "education";
                            else if (model.FreeGmBitcoin)
                                freeContentCategory = "gm-bitcoin";

                            if (membersCategory.Length > 0 || freeContentCategory.Length > 0)
                            {
                                string videoUrl = $"{Request.Scheme}://{Request.Host}/login?returnUrl={(membersCategory != null ? "members" : "free-content")}/{membersCategory ?? freeContentCategory}/{videoId}/{model.Title.UrlFriendly()}";
                                
                                List<Task> tasks = new();

                                foreach (var emailSubscriber in emailSubscribedUsers)
                                {
                                    string unsubscribeUrl = $"{Request.Scheme}://{Request.Host}/unsubscribe/{emailSubscriber.Id}";
                                    string html = await this.RenderViewToStringAsync($"/Views/EmailTemplates/NewVideoTemplate.cshtml", new Core.Models.NewVideoEmailModel(model.Title, model.Details, videoUrl, unsubscribeUrl), true);

                                    tasks.Add(_emailService.Send(emailSubscriber.Email, "New Video Uploaded!", html));
                                }

                                await Task.WhenAll(tasks);
                            }
                        }
                    }
                }

                return LocalRedirect("/admin/videos");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> CreateUser(AddUserFormModel model)
        {
            try
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, model.Username, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.PackageId.HasValue)
                    {
                        var package = await _packagesRepository.GetPackage(model.PackageId.Value);

                        if (package != null)
                        {
                            var orderId = await _ordersRepository.CreateOrder(model.PackageId.Value, package.Price, user.Id, true);

                            if (orderId > 0)
                                await _subscriptionsRepository.CreateSubscription(user.Id, model.PackageId.Value, orderId);
                        }
                    }     
                }

                return LocalRedirect("/admin/users");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<JsonResult> AddUserToAdmin(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");

                    return new JsonResult(true);
                } 
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }

            return new JsonResult(false);
        }

        public async Task<JsonResult> RemoveUserFromAdmin(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, "Admin");

                    return new JsonResult(true);
                }
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }

            return new JsonResult(false);
        }
        #endregion

        #region PUT
        public async Task<IActionResult> UpdatePackage(EditPackageFormModel model)
        {
            try
            {
                if (model.Title != null && model.Price.HasValue)
                    await _adminRepository.UpdatePackage(model.Id, model.Title, model.Price.Value, model.Duration, model.DurationType, model.Description);

                return LocalRedirect("/admin/packages");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> UpdateVideo(EditVideoFormModel model)
        {
            try
            {
                if (model.VideoUrl != null && model.Title != null && model.Details != null)
                    await _adminRepository.UpdateVideo(model.Id, model.VideoUrl, model.Title, model.Details, model.MarketReview, model.Resources, model.MembersEducation, model.TodaysReview, model.FreeGmBitcoin, model.FreeEducation);

                return LocalRedirect("/admin/videos");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> UpdateHomePage(Views.Admin.Pages.Home.IndexModel model)
        {
            try
            {
                List<Task> tasks = new List<Task>();
                if (model.PageSections.Any())
                {
                    model.PageSections.ForEach(pageSection =>
                    {
                        if (pageSection.Contents.Any())
                        {
                            pageSection.Contents.ForEach(pageContent =>
                            {
                                tasks.Add(_pageContentRepository.UpdatePageSectionContent(pageContent.Id, pageContent.Content, pageContent.Header));
                            });
                        }
                    });
                }

                await Task.WhenAll(tasks);

                return LocalRedirect("/admin/pages/home");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> UpdatePageContent(Core.Models.PageContent model)
        {
            try
            {
                await _pageContentRepository.UpdatePageSectionContent(model.Id, model.Content, model.Header);
                string returnUrl = "/admin/home";

                switch (model.Header)
                {
                    case "Terms & Conditions":
                        returnUrl = "/admin/pages/terms-and-conditions";
                        break;
                    case "Disclaimer":
                        returnUrl = "/admin/pages/disclaimer";
                        break;
                    case "About":
                        returnUrl = "/admin/pages/about";
                        break;
                }

                return LocalRedirect(returnUrl);
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }
        #endregion

        #region DELETE
        public async Task<IActionResult> DeleteChart(int id)
        {
            try
            {
                var chart = await _chartsRepository.GetChart(id);

                if (chart != null)
                {
                    await _adminRepository.DeleteChart(id);
                    await _azureStorageRepository.Delete(chart.ImageUrl, "charts");
                }

                return LocalRedirect("/admin/charts");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> DeletePackage(int id)
        {
            try
            {
                await _adminRepository.DeletePackage(id);

                return LocalRedirect("/admin/packages");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }

        }

        public async Task<IActionResult> DeleteVideo(int id)
        {
            try
            {
                await _adminRepository.DeleteVideo(id);

                return LocalRedirect("/admin/videos");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }

        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user != null)
                {
                    await _userManager.DeleteAsync(user);

                    var subscription = await _subscriptionsRepository.GetSubscription(id);

                    if (subscription != null)
                        await _adminRepository.DeleteSubscription(subscription.Id);
                }

                return LocalRedirect("/admin/users");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                await _adminRepository.DeleteSubscription(id);

                return LocalRedirect("/admin/orders");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }

        }
        #endregion

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}

