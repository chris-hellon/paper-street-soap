using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Interfaces.Services;
using PaperStreetSoap.Core.Interfaces.Repositories;
using PaperStreetSoap.Core.Models;
using ProjectBase.Core.Utils;
using System.Security.Claims;

namespace PaperStreetSoap.Controllers
{
    public class FreeContentController : Controller
    {
        private readonly IErrorLoggerService _errorLoggerService;
        private readonly IVideosRepository _videosRepository;
        private readonly IChartsRepository _chartsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FreeContentController(IErrorLoggerService errorLoggerService, IVideosRepository videosRepository, IChartsRepository chartsRepository, IHttpContextAccessor httpContextAccessor)
        {
            _errorLoggerService = errorLoggerService;
            _videosRepository = videosRepository;
            _httpContextAccessor = httpContextAccessor;
            _chartsRepository = chartsRepository;
        }

        #region GET
        [Route("/free-content/{category}")]
        public async Task<IActionResult> Index(string category)
        {
            try
            {
                ViewData["Category"] = category;
                ViewData["Section"] = "FreeContent";

                string[] categories = new string[] { "education", "gm-bitcoin", "charts" };

                if (categories.Contains(category))
                {
                    IEnumerable<Video> videos = null;
                    IEnumerable<Chart> charts = null;

                    switch (category)
                    {
                        case "education":
                            ViewData["Title"] = "Education";
                            videos = await _videosRepository.GetFreeEducation();
                            break;
                        case "gm-bitcoin":
                            ViewData["Title"] = "Gm Bitcoin";
                            videos = await _videosRepository.GetGmBitcoin();
                            break;
                        case "charts":
                            ViewData["Title"] = "Charts";
                            charts = await _chartsRepository.GetCharts();
                            break;
                    }

                    return View(new Views.FreeContent.IndexModel(videos, charts));
                }

                return LocalRedirect("/error/404");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }

        [Route("/free-content/{category}/{id}/{videoTitle}")]
        public async Task<IActionResult> Video(string category, int? id, string videoTitle)
        {
            try
            {
                ViewData["Section"] = "FreeContent";

                if (id.HasValue)
                {
                    bool isEducation = category == "education";
                    bool isGmBitcoin = category == "gm-bitcoin";

                    Video video = null;

                    if (isEducation)
                        video = await _videosRepository.GetFreeEducationVideo(id.Value);
                    else if (isGmBitcoin)
                        video = await _videosRepository.GetGmBitcoinVideo(id.Value);

                    if (video != null && video.Title.UrlFriendly() == videoTitle)
                    {
                        ViewData["Title"] = video != null ? video.Title : isEducation ? "Education" : "Gm Bitcoin";

                        return View("Video/Index", new Views.FreeContent.Video.IndexModel(video));
                    }
                }

                return LocalRedirect("/error/404");
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }
        #endregion

        #region POST
        [Route("/free-content/createvideodiscussion")]
        public async Task<JsonResult> CreateVideoDiscussion(Views.Shared.VideoDiscussionAddCommentModel model)
        {
            ViewData["Section"] = "FreeContent";
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

