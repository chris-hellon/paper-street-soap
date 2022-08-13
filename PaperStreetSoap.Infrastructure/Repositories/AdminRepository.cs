using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Core.Utils;
using PaperStreetSoap.Core.Interfaces.Repositories;

namespace PaperStreetSoap.Infrastructure.Repositories
{
    public class AdminRepository : SubscriptionsRepository, IAdminRepository
    {
        public AdminRepository(IDapperContext dapperContext, IPackagesRepository packagesRepository) : base(dapperContext, packagesRepository)
        {
        }

        #region Charts
        public async Task CreateChart(string title, string imageUrl, bool liveTrading)
        {
            await DapperConnection.ExecuteInsertStoredProcedureSingle("CreateChart", new
            {
                Title = title,
                ImageUrl = imageUrl,
                LiveTrading = liveTrading
            });
        }

        public async Task DeleteChart(int id)
        {
            await DapperConnection.ExecuteDeleteStoredProcedureSingle("DeleteChart", new
            {
                Id = id
            });
        }
        #endregion

        #region Packages
        public async Task CreatePackage(string title, decimal price, int duration, string durationType, string? description = null, string? openNodeKey = null)
        {
            await DapperConnection.ExecuteInsertStoredProcedureSingle("CreatePackage",
                new
                {
                    Title = title,
                    Price = price,
                    Description = description,
                    Duration = duration,
                    DurationType = durationType,
                    OpenNodeKey = openNodeKey
                });
        }

        public async Task DeletePackage(int id)
        {
            await DapperConnection.ExecuteDeleteStoredProcedureSingle("DeletePackage", new
            {
                Id = id
            });
        }

        public async Task UpdatePackage(int id, string title, decimal price, int duration, string durationType, string? description = null, string? openNodeKey = null)
        {
            await DapperConnection.ExecuteUpdateStoredProcedureSingle("UpdatePackage", new
            {
                Id = id,
                Title = title,
                Price = price,
                Duration = duration,
                DurationType = durationType,
                Description = description,
                OpenNodeKey = openNodeKey
            });
        }
        #endregion

        #region Videos
        public async Task<int> CreateVideo(string videoUrl, string title, string details, bool? marketReview = null, bool? resources = null, bool? membersEducation = null, bool? todaysReview = null, bool? gmBitcoin = null, bool? freeEducation = null)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<int>("CreateVideo",
                new
                {
                    VideoUrl = videoUrl,
                    Title = title,
                    Details = details,
                    MarketReview = marketReview,
                    Resources = resources,
                    MembersEducation = membersEducation,
                    TodaysReview = todaysReview,
                    FreeGmBitcoin = gmBitcoin,
                    FreeEducation = freeEducation
                });
        }

        public async Task DeleteVideo(int id)
        {
            await DapperConnection.ExecuteDeleteStoredProcedureSingle("DeleteVideo", new {
                Id = id
            });
        }

        public async Task UpdateVideo(int id, string videoUrl, string title, string details, bool? marketReview = null, bool? resources = null, bool? membersEducation = null, bool? todaysReview = null, bool? gmBitcoin = null, bool? freeEducation = null)
        {
            await DapperConnection.ExecuteUpdateStoredProcedureSingle("UpdateVideo",
                new
                {
                    Id = id,
                    VideoUrl = videoUrl,
                    Title = title,
                    Details = details,
                    MarketReview = marketReview,
                    Resources = resources,
                    MembersEducation = membersEducation,
                    TodaysReview = todaysReview,
                    GmBitcoin = gmBitcoin,
                    FreeEducation = freeEducation
                });
        }
        #endregion

    }
}

