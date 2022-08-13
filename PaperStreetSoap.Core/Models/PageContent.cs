using System.ComponentModel.DataAnnotations;

namespace PaperStreetSoap.Core.Models
{
    public class PageContent
    {
        public int Id { get; set; }
        public int PageSectionId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Header { get; set; } = null;
    }
}

