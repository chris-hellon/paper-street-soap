using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Interfaces.Services;
using PaperStreetSoap.Core.Interfaces.Repositories;

namespace PaperStreetSoap.Controllers
{
    public class PageContentController : Controller
    {
        protected int PageSectionId { get; set; }
        private readonly IErrorLoggerService _errorLoggerService;
        private readonly IPageContentRepository _pageContentRepository;

        public PageContentController(IErrorLoggerService errorLoggerService, IPageContentRepository pageContentRepository, int pageSectionId)
        {
            _errorLoggerService = errorLoggerService;
            _pageContentRepository = pageContentRepository;
            PageSectionId = pageSectionId;
        }

        public virtual async Task<IActionResult> Index()
        {
            try
            {
                var pageContent = await _pageContentRepository.GetPageSectionContent(PageSectionId);
                return View(pageContent.FirstOrDefault());
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
                return LocalRedirect("/error");
            }
        }
    }
}

