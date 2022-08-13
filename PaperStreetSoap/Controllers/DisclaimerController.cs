using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Interfaces.Services;
using PaperStreetSoap.Core.Interfaces.Repositories;

namespace PaperStreetSoap.Controllers
{
    public class DisclaimerController : PageContentController
    {
        public DisclaimerController(IErrorLoggerService errorLoggerService,  IPageContentRepository pageContentRepository) : base(errorLoggerService, pageContentRepository, 5)
        {

        }

        [Route("/disclaimer")]
        public override async Task<IActionResult> Index()
        {
            return await base.Index();
        }
    }
}

