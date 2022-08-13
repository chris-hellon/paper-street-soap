namespace PaperStreetSoap.Views.FreeContent.Video
{
    public class IndexModel
    {
        public Core.Models.Video? Video { get; set; }

        public IndexModel(Core.Models.Video? video)
        {
            Video = video;
        }
    }
}
