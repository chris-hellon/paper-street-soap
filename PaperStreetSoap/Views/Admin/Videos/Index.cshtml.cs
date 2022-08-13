using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Views.Admin.Partials;

namespace PaperStreetSoap.Views.Admin.Videos
{
    public class IndexModel
    {
        public IEnumerable<Video>? Videos { get; set; }
        public AddVideoFormModel Video { get; set; } = new AddVideoFormModel();

        public IndexModel(IEnumerable<Video>? videos)
        {
            Videos = videos;
        }
    }
}
