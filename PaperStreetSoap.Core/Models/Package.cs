namespace PaperStreetSoap.Core.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public decimal Price { get; set; }
        public string BtcPayServerKey { get; set; } = string.Empty;
        public bool Admin { get; set; } = false;
        public double WowDelay { get; set; } = 0.1;
        public int Duration { get; set; }
        public string DurationType { get; set; } = string.Empty;
        public string OpenNodeKey { get; set; }
        public string CssClass { get; set; } = "col-lg-4 col-md-6";

        public Package()
        {

        }

        public Package(string title, decimal price, string? description = null)
        {
            Title = title;
            Description = description;
            Price = price;
        }
    }
}

