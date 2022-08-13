
namespace PaperStreetSoap.Views.Orders.Confirmation
{
    public class IndexModel
    {
        public int OrderId { get; set; }
        public string PackageName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IndexModel(int orderId, string packageName, DateTime startDate, DateTime endDate)
        {
            OrderId = orderId;
            PackageName = packageName;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
