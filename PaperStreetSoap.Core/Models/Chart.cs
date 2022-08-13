namespace PaperStreetSoap.Core.Models
{
    public class Chart
    {
        public int Id { get; set; }
        public string? Title { get; set; } = null;
        public string? NavigateUrl { get; set; } = null;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public double WowDelay { get; set; } = 0.1;
        public bool Admin { get; set; } = false;
        public string CssClass { get; set; } = "col-lg-6 col-md-12 col-sm-12";

        public Chart()
        {

        }
        public Chart(string title, string imageUrl, DateTime date, string? navigateUrl = null)
        {
            Title = title;
            Date = date;
            NavigateUrl = navigateUrl;
            ImageUrl = imageUrl;
        }
    }
}

