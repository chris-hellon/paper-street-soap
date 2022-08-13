namespace PaperStreetSoap.Views.Orders.Fail
{
    public class IndexModel
    {
        public string OpenNodeId { get; set; }
        public int OrderId { get; set; }
        public string PackageName { get; set; }

        public IndexModel(string openNodeId, int orderId, string packageName)
        {
            OpenNodeId = openNodeId;
            OrderId = orderId;
            PackageName = packageName;
        }
    }
}
