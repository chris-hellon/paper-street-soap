using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Views.Members.LatestReview
{
    public class IndexModel : ViewModels.MembersViewModel
    {
        public Core.Models.Video? Video { get; set; }

        public IndexModel(Core.Models.Video? video, Subscription? subscription) : base (subscription)
        {
            Video = video;
        }
    }
}
