namespace PaperStreetSoap.Core.Models
{
    public class About
    {
        public string Icon { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public About(string icon, string title, string description)
        {
            Icon = icon;
            Title = title;
            Description = description;
        }
    }
}

