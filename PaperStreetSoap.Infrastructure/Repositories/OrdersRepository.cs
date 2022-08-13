using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using PaperStreetSoap.Core.Interfaces.Repositories;
using ProjectBase.Core.Utils;
using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Infrastructure.Repositories
{
    public class OrdersRepository : BaseRepository, IOrdersRepository
    {
        public OrdersRepository(IDapperContext dapperContext) : base(dapperContext)
        {
            
        }

        public async Task<int> CreateOrder(int packageId, decimal amount, string userId, bool orderConfirmed = false)
        {
            return await DapperConnection.ExecuteInsertStoredProcedureSingle("CreateOrder", new
            {
                UserId = userId,
                PackageId = packageId,
                Amount = amount,
                OrderConfirmed = orderConfirmed
            });
        }

        public async Task<int> UpdateOrder(int orderId, string openNodeId, bool orderConfirmed = false)
        {
            return await DapperConnection.ExecuteUpdateStoredProcedureSingle("UpdateOrder", new
            {
                Id = orderId,
                OpenNodeId = openNodeId,
                OrderConfirmed = orderConfirmed
            });
        }

        public async Task DeleteOrder(int orderId)
        {
            await DapperConnection.ExecuteDeleteStoredProcedureSingle("DeleteOrder", new
            {
                OrderId = orderId
            });
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<Order>("GetOrders");
        }

        public async Task<Order> GetOrder(int orderId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<Order>("GetOrder", new
            {
                OrderId = orderId
            });
        }

        public async Task<Order> GetUsersOutstandingOrder(string userId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<Order>("GetUsersOutstandingOrder", new
            {
                UserId = userId
            });
        }
    }
}

