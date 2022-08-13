namespace PaperStreetSoap.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public bool OrderConfirmed { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AmendDate { get; set; }
        public string OpenNodeId { get; set; }
        public string UserName { get; set; }
        public string PackageName { get; set; } = string.Empty;
    }
}

