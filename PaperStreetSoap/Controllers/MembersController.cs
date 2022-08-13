using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectBase.Core.Interfaces.Services;
using PaperStreetSoap.Core.Interfaces.Repositories;
using System.Security.Claims;
using PaperStreetSoap.Core.Models;
using ProjectBase.Core.Utils;

namespace PaperStreetSoap.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        protected readonly IVideosRepository _videosRepository;
        protected readonly IChartsRepository _chartsRepository;
        protected readonly ISubscriptionsRepository _subscriptionsRepository;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IErrorLoggerService _errorLoggerService;

        public MembersController(IErrorLoggerService errorLoggerService, IVideosRepository videosRepository, ISubscriptionsRepository subscriptionsRepository, IChartsRepository chartsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _errorLoggerService = errorLoggerService;
            _videosRepository = videosRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _httpContextAccessor = httpContextAccessor;
            _chartsRepository = chartsRepository;
        }

        #region GET
        [Route("/members/latest-review")]
        public async Task<IActionResult> LatestReview()
        {
            try
            {
                ViewData["Section"] = "Members";

                var subscription = await GetSubscription();
                if (subscription == null)
                    return RedirectToSubscription();

                return View("LatestReview/Index", new Views.Members.LatestReview.IndexModel(await _videosRepository.GetTodaysReview(), subscription));
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/members/{category}")]
        public async Task<IActionResult> Category(string category)
        {
            try
            {
                ViewData["Section"] = "Members";
                ViewData["Category"] = category;

                var subscription = await GetSubscription();
                if (subscription == null)
                    return RedirectToSubscription();

                var categories = new string[] { "market-reviews", "education", "resources", "live-trading" };
                IEnumerable<Video> videos = null;
                Chart liveTradingChart = null;

                if (categories.Contains(category))
                {
                    switch (category)
                    {
                        case "market-reviews":
                            ViewData["Title"] = "Market Reviews";
                            videos = await _videosRepository.GetMarketReviews();
                            break;
                        case "education":
                            ViewData["Title"] = "Education";
                            videos = await _videosRepository.GetMembersEducation();
                            break;
                        case "resources":
                            ViewData["Title"] = "Resources";
                            videos = await _videosRepository.GetResources();
                            break;
                        case "live-trading":
                            ViewData["Title"] = "Live Trading";
                            liveTradingChart = await _chartsRepository.GetLiveTradingChart();
                            break;
                    }
                }

                return View("Category/Index", new Views.Members.Category.IndexModel(subscription, videos, liveTradingChart));
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/members/{category}/{id}/{videoTitle}")]
        public async Task<IActionResult> Video(string category, int? id, string videoTitle)
        {
            try
            {
                ViewData["Section"] = "Members";

                var subscription = await GetSubscription();
                if (subscription == null)
                    return RedirectToSubscription();

                Video video = null;

                if (id.HasValue)
                {
                    video = await _videosRepository.GetVideo(id.Value);

                    if (video != null && video.Title.UrlFriendly() == videoTitle)
                        ViewData["Title"] = video.Title;
                }

                return View("Video/Index", new Views.Members.Video.IndexModel(video, subscription));
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        public async Task<Subscription> GetSubscription()
        {
            string userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
                return await _subscriptionsRepository.GetSubscription(userId);

            return null;
        }

        public IActionResult RedirectToSubscription()
        {
            return LocalRedirect("/members/subscription");
        }
        #endregion

        #region POST
        [Route("/members/createvideodiscussion")]
        public async Task<JsonResult> CreateVideoDiscussion(Views.Shared.VideoDiscussionAddCommentModel model)
        {
            ViewData["Section"] = "Members";
            string userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            try
            {
                if (userId != null && model.Comment != null)
                {
                    int videoDiscussionId = await _videosRepository.CreateVideoDiscussion(model.VideoId, userId, model.Comment, model.ParentId);
                    var videoDiscussion = await _videosRepository.GetVideoDiscussion(videoDiscussionId);

                    string viewName = model.ParentId.HasValue ? "_VideoDiscussionNestedPartial.cshtml" : "_VideoDiscussionPartial.cshtml";
                    string html = await this.RenderViewToStringAsync($"/Views/Shared/{viewName}", videoDiscussion, true);

                    return new JsonResult(new
                    {
                        Html = html,
                        ParentId = model.ParentId
                    });
                }
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }

            return new JsonResult(false);
        }
        #endregion
    }
}

