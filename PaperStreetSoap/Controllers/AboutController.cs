using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Interfaces.Services;
using PaperStreetSoap.Core.Interfaces.Repositories;

namespace PaperStreetSoap.Controllers
{
    public class AboutController : PageContentController
    {
        public AboutController(IErrorLoggerService errorLoggerService, IPageContentRepository pageContentRepository) : base(errorLoggerService, pageContentRepository, 3)
        {
        }

        [Route("/about")]
        public override async Task<IActionResult> Index()
        {
            return await base.Index();
        }
    }
}

