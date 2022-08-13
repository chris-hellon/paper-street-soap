using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Core.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<IEnumerable<EmailSubscribedUsers>> GetEmailSubscribedUsers();

        Task<bool> IsUserEmailSubscribed(string userId);

        Task UnsubscribeUser(string id);

        Task SubscribeUser(string id);
    }
}

