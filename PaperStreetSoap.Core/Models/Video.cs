namespace PaperStreetSoap.Core.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public bool MarketReview { get; set; }
        public bool Resources { get; set; }
        public bool FreeGmBitcoin { get; set; }
        public bool FreeEducation { get; set; }
        public bool MembersEducation { get; set; }
        public bool TodaysReview { get; set; }
        public DateTime Date { get; set; }
        public double WowDelay { get; set; } = 0.1;
        public bool Admin { get; set; } = false;
        public string CssClass { get; set; } = "col-lg-6 col-sm-12";
        public List<VideoDiscussion> VideoDiscussions { get; set; } = new List<VideoDiscussion>();

        public Video()
        {
        }
    }
}

