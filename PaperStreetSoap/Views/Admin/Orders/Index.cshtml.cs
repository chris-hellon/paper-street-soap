using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Views.Admin.Orders
{
    public class IndexModel
    {
        public IEnumerable<Order> Orders { get; set; }

        public IndexModel(IEnumerable<Order> orders)
        {
            Orders = orders;
        }
    }
}
