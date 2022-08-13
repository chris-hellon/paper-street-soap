using Microsoft.AspNetCore.Mvc.RazorPages;
using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace PaperStreetSoap.Areas.Identity.Pages.Account
{
    [Authorize]
    public class SelectPackageModel : PageModel
    {
        public IEnumerable<Package>? Packages { get; set; }
        private readonly IPackagesRepository _packagesRepository;

        public SelectPackageModel(IPackagesRepository packagesRepository)
        {
            _packagesRepository = packagesRepository;
        }

        public async Task OnGetAsync()
        {
            Packages = await _packagesRepository.GetPackages();
        }
    }
}
