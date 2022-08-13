namespace PaperStreetSoap.Views.FreeContent
{
    public class IndexModel
    {
        public IEnumerable<Core.Models.Video>? Videos { get; set; }
        public IEnumerable<Core.Models.Chart>? Charts { get; set; }

        public IndexModel(IEnumerable<Core.Models.Video>? videos = null, IEnumerable<Core.Models.Chart>? charts = null)
        {
            Videos = videos;
            Charts = charts;
        }
    }
}
