using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Core.Interfaces.Repositories
{
    public interface ISubscriptionsRepository
    {
        Task<Subscription> GetSubscription(string userId);

        Task<IEnumerable<Subscription>> GetSubscriptions();

        Task<int> CreateSubscription(string userId, int packageId, int orderId);

        Task DeleteSubscription(int subscriptionId);
    }
}
