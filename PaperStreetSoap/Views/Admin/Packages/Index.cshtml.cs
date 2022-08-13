using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Views.Admin.Partials;

namespace PaperStreetSoap.Views.Admin.Packages
{
    public class IndexModel
    {
        public IEnumerable<Package>? Packages { get; set; }
        public AddPackageFormModel Package { get; set; } = new AddPackageFormModel();

        public IndexModel(IEnumerable<Package>? packages)
        {
            Packages = packages;
        }
    }
}
