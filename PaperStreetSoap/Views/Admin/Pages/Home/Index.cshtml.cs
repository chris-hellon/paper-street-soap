using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Views.Admin.Pages.Home
{
    public class IndexModel
    {
        public List<PageSection> PageSections { get; set; }

        public IndexModel()
        {

        }

        public IndexModel(IEnumerable<PageSection> pageSections)
        {
            PageSections = pageSections.ToList();
        }
    }
}
