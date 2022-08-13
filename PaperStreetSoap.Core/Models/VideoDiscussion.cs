namespace PaperStreetSoap.Core.Models
{
    public class VideoDiscussion
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int? ParentId { get; set; }
        public string ProfileImageUrl { get; set; } = string.Empty;

        public List<VideoDiscussion> NestedDiscussions { get; set; } = new List<VideoDiscussion>();

        public VideoDiscussion()
        {
        }
    }
}

