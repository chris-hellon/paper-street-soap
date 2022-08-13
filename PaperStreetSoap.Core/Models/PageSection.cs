namespace PaperStreetSoap.Core.Models
{
    public class PageSection
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public List<PageContent> Contents { get; set; }
    }
}

