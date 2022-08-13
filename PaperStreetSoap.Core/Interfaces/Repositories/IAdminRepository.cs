using PaperStreetSoap.Core.Models;
namespace PaperStreetSoap.Core.Interfaces.Repositories
{
    public interface IAdminRepository : ISubscriptionsRepository
    {
        Task CreateChart(string title, string imageUrl, bool liveTrading);

        Task DeleteChart(int id);

        Task CreatePackage(string title, decimal price, int duration, string durationType, string? description = null, string? openNodeKey = null);

        Task DeletePackage(int id);

        Task UpdatePackage(int id, string title, decimal price, int duration, string durationType, string? description = null, string? openNodeKey = null);

        Task<int> CreateVideo(string videoUrl, string title, string details, bool? marketReview = null, bool? resources = null, bool? membersEducation = null, bool? todaysReview = null, bool? gmBitcoin = null, bool? freeEducation = null);

        Task DeleteVideo(int id);

        Task UpdateVideo(int id, string videoUrl, string title, string details, bool? marketReview = null, bool? resources = null, bool? membersEducation = null, bool? todaysReview = null, bool? gmBitcoin = null, bool? freeEducation = null);

        Task<IEnumerable<User>> GetUsers();
    }
}

