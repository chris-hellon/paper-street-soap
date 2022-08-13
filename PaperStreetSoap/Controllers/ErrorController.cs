using System.Diagnostics;
using ProjectBase.Core.Interfaces.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PaperStreetSoap.Views.Error;

namespace PaperStreetSoap.Controllers
{
    public class ErrorController : Controller
    {
        private readonly IErrorLoggerService _errorLoggerService;

        public ErrorController(IErrorLoggerService errorLoggerService)
        {
            _errorLoggerService = errorLoggerService;
        }

        [Route("/error/{statusCode?}")]
        public IActionResult Index(int? statusCode = null)
        {
            var errorModel = new IndexModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var errorMessage = string.Empty;

            if (statusCode.HasValue)
            {
                switch (statusCode.Value)
                {
                    case 404:
                        ViewData["Title"] = "Page not found";
                        errorMessage = "Oops, we can't find that page.";
                        break;
                    case 401:
                        ViewData["Title"] = "Unauthorized";
                        errorMessage = "You are not authorized to access this page.";
                        break;
                    default:
                        ViewData["Title"] = "Error";
                        errorMessage = "An error occurred while processing your request.";
                        break;
                }
            }

            errorModel.ExceptionMessage = exceptionHandlerPathFeature?.Error?.Message ?? errorMessage;

            return View(errorModel);
        }
    }
}

