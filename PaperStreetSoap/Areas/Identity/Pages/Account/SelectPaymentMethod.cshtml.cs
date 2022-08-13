using Microsoft.AspNetCore.Mvc.RazorPages;
using PaperStreetSoap.Core.Interfaces.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using ProjectBase.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace PaperStreetSoap.Areas.Identity.Pages.Account
{
    public class SelectPaymentMethodModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPackagesRepository _packagesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public int PackageId { get; set; }
        public string? CustomerEmail { get; set; } = null;
        public string? ChoiceKey { get; set; } = null;
        public string? NotificationUrl { get; set; } = null;
        public string? RedirectUrl { get; set; } = null;

        public SelectPaymentMethodModel(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IPackagesRepository packagesRepository)
        {
            _packagesRepository = packagesRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int packageId)
        {
            PackageId = packageId;

            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId != null)
            {
                RedirectUrl = $"{this.Request.Scheme}://{this.Request.Host}/order-confirmation/{packageId}/{userId}";
                NotificationUrl = $"{this.Request.Scheme}://{this.Request.Host}/order-status";

                var package = await _packagesRepository.GetPackage(packageId);

                if (package != null)
                {
                    ChoiceKey = package.Title;

                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        CustomerEmail = user.Email;
                        return Page();
                    }
                }
            }

            return LocalRedirect("/error");

        }
    }
}
