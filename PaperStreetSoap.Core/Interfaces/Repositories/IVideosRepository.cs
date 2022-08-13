using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Core.Interfaces.Repositories
{
    public interface IVideosRepository
    {
        Task<Video> GetTodaysReview();

        Task<IEnumerable<Video>> GetMarketReviews(bool admin = false);

        Task<IEnumerable<Video>> GetMembersEducation(bool admin = false);

        Task<IEnumerable<Video>> GetResources(bool admin = false);

        Task<IEnumerable<Video>> GetGmBitcoin(bool admin = false);

        Task<IEnumerable<Video>> GetFreeEducation(bool admin = false);

        Task<IEnumerable<Video>> GetAllVideos(bool admin = false);

        Task<Video> GetVideo(int id);

        Task<Video> GetGmBitcoinVideo(int id);

        Task<Video> GetFreeEducationVideo(int id);

        Task<IEnumerable<VideoDiscussion>> GetVideoDiscussions(int id);

        Task<VideoDiscussion> GetVideoDiscussion(int id);

        Task<int> CreateVideoDiscussion(int videoId, string userId, string comments, int? parentId = null);
    }
}

