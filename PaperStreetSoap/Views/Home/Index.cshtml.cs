using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Views.Home
{
    public class IndexModel
    {
        public IEnumerable<PageContent> About { get; set; }
        public IEnumerable<Chart> Charts { get; set; }
        public IEnumerable<PageContent> WelcomeVideo { get; set; }
        public IEnumerable<PageContent> ShortIntroduction { get; set; }

        public IndexModel(IEnumerable<PageContent> about, IEnumerable<Chart> charts, IEnumerable<PageContent> welcomeVideo, IEnumerable<PageContent> shortIntroduction)
        {
            About = about;
            Charts = charts;
            WelcomeVideo = welcomeVideo;
            ShortIntroduction = shortIntroduction;
        }
    }
}
