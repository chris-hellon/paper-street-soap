using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Core.Utils;
using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Core.Interfaces.Repositories;
using ProjectBase.Infrastructure.Repositories;

namespace PaperStreetSoap.Infrastructure.Repositories
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<User>("GetUsers");
        }

        public async Task<IEnumerable<EmailSubscribedUsers>> GetEmailSubscribedUsers()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<EmailSubscribedUsers>("GetEmailSubscribedUsers");
        }

        public async Task<bool> IsUserEmailSubscribed(string userId)
        {
            var subscribedUser = await DapperConnection.ExecuteGetStoredProcedureSingle<EmailSubscribedUsers>("GetEmailSubscribedUsers", new
            {
                UserId = userId
            });

            return subscribedUser != null;
        }

        public async Task UnsubscribeUser(string id)
        {
            await DapperConnection.ExecuteUpdateStoredProcedureSingle("UnsubscribeUser", new
            {
                Id = id
            });
        }

        public async Task SubscribeUser(string id)
        {
            await DapperConnection.ExecuteUpdateStoredProcedureSingle("SubscribeUser", new
            {
                Id = id
            });
        }
    }
}

