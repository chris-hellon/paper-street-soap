namespace PaperStreetSoap.Core.Models
{
    public class NewVideoEmailModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UnsubscribeUrl { get; set; }

        public NewVideoEmailModel(string title, string description, string url, string unsubscribeUrl)
        {
            Title = title;
            Description = description;
            Url = url;
            UnsubscribeUrl = unsubscribeUrl;
        }
    }
}

