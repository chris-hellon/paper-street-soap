namespace PaperStreetSoap.Views.Orders.Processing
{
    public class IndexModel
    {
        public int OrderId { get; set; }
        public string PackageName { get; set; }

        public IndexModel(int orderId, string packageName)
        {
            OrderId = orderId;
            PackageName = packageName;
        }
    }
}
