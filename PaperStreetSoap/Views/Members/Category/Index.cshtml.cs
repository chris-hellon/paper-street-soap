using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Views.Members.Category
{
    public class IndexModel : ViewModels.MembersViewModel
    {
        public IEnumerable<Core.Models.Video>? Videos { get; set; }
        public Chart? LiveTradingChart { get; set; }

        public IndexModel(Subscription? subscription, IEnumerable<Core.Models.Video>? videos = null, Chart? liveTradingChart = null) : base(subscription)
        {
            Videos = videos;
            LiveTradingChart = liveTradingChart;
        }
    }
}
