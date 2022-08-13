using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Core.Utils;
using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Core.Interfaces.Repositories;

namespace PaperStreetSoap.Infrastructure.Repositories
{
    public class SubscriptionsRepository : UsersRepository, ISubscriptionsRepository
    {
        private readonly IPackagesRepository _packagesRepository;

        public SubscriptionsRepository(IDapperContext dapperContext, IPackagesRepository packagesRepository) : base (dapperContext)
        {
            _packagesRepository = packagesRepository;
        }

        public async Task<Subscription> GetSubscription(string userId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<Subscription>("GetUserSubscription", new
            {
                UserId = userId
            });
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptions()
        {
            return await DapperConnection.GetList<Subscription>("GetSubscriptions");
        }

        public async Task<int> CreateSubscription(string userId, int packageId, int orderId)
        {
            var package = await _packagesRepository.GetPackage(packageId);

            var startDate = DateTime.Now;
            var endDate = DateTime.Now;

            switch (package.DurationType)
            {
                case "Month":
                    endDate = startDate.AddMonths(package.Duration);
                    break;
                case "Year":
                    endDate = startDate.AddYears(package.Duration);
                    break;
            }

            return await DapperConnection.ExecuteInsertStoredProcedureSingle("CreateSubscription", new
            {
                UserId = userId,
                PackageId = packageId,
                StartDate = startDate,
                EndDate = endDate,
                OrderId = orderId
            });
        }

        public async Task DeleteSubscription(int subscriptionId)
        {
            await DapperConnection.ExecuteDeleteStoredProcedureSingle("DeleteSubscription", new
            {
                Id = subscriptionId
            });
        }
    }
}

