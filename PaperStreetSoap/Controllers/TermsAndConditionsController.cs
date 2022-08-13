using Microsoft.AspNetCore.Mvc;
using ProjectBase.Core.Interfaces.Services;
using PaperStreetSoap.Core.Interfaces.Repositories;

namespace PaperStreetSoap.Controllers
{
    public class TermsAndConditionsController : PageContentController
    {
        public TermsAndConditionsController(IErrorLoggerService errorLoggerService, IPageContentRepository pageContentRepository) : base(errorLoggerService, pageContentRepository, 4)
        {

        }

        [Route("/terms-and-conditions")]
        public override async Task<IActionResult> Index()
        {
            return await base.Index();
        }
    }
}

