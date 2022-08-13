using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Views.Admin.Partials;

namespace PaperStreetSoap.Views.Admin.Charts
{
    public class IndexModel
    {
        public IEnumerable<Chart>? Charts { get; set; }

        public AddChartFormModel Chart { get; set; } = new AddChartFormModel();

        public IndexModel(IEnumerable<Chart>? charts)
        {
            Charts = charts;
        }
    }
}
