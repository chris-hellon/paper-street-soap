using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Core.Interfaces.Repositories
{
    public interface IOrdersRepository
    {
        Task<int> CreateOrder(int packageId, decimal amount, string userId, bool orderConfirmed = false);

        Task<int> UpdateOrder(int orderId, string openNodeId, bool orderConfirmed = false);

        Task DeleteOrder(int orderId);

        Task<IEnumerable<Order>> GetOrders();

        Task<Order> GetOrder(int orderId);

        Task<Order> GetUsersOutstandingOrder(string userId);
    }
}

