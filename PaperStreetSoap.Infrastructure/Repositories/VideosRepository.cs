using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Core.Utils;
using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Core.Interfaces.Repositories;

namespace PaperStreetSoap.Infrastructure.Repositories
{
    public class VideosRepository : BaseRepository, IVideosRepository
    {
        public VideosRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

        public async Task<Video> GetTodaysReview()
        {
            var video = await DapperConnection.ExecuteGetStoredProcedureSingle<Video>("GetVideos", new
            {
                TodaysReview = true
            });

            if (video != null)
            {
                video.CssClass = "col-lg-12 col-sm-12";

                var videoDiscussions = await GetVideoDiscussions(video.Id);

                if (videoDiscussions != null)
                {
                    foreach (var videoDiscussion in videoDiscussions)
                    {
                        if (videoDiscussion.ParentId.HasValue)
                        {
                            var parentVideoDiscussion = videoDiscussions.SingleOrDefault(w => w.Id == videoDiscussion.ParentId.Value);

                            if (parentVideoDiscussion != null)
                                parentVideoDiscussion.NestedDiscussions.Add(videoDiscussion);
                        }
                        else
                            video.VideoDiscussions.Add(videoDiscussion);
                    }
                }
            }

            return video;
        }

        private async Task<IEnumerable<Video>> GetVideos(bool admin = false, object? request = null)
        {
            var videos = await DapperConnection.ExecuteGetStoredProcedureList<Video>("GetVideos", request);

            if (!admin && videos.Any())
                videos.First().CssClass = "col-lg-12 col-sm-12";

            return videos;
        }

        public async Task<IEnumerable<Video>> GetAllVideos(bool admin = false)
        {
            return await GetVideos(admin);
        }

        public async Task<IEnumerable<Video>> GetMarketReviews(bool admin = false)
        {
            return await GetVideos(admin, new
            {
                MarketReview = true
            });
        }

        public async Task<IEnumerable<Video>> GetResources(bool admin = false)
        {
            return await GetVideos(admin, new
            {
                Resources = true
            });
        }

        public async Task<IEnumerable<Video>> GetMembersEducation(bool admin = false)
        {
            return await GetVideos(admin, new
            {
                MembersEducation = true
            });
        }

        public async Task<IEnumerable<Video>> GetGmBitcoin(bool admin = false)
        {
            return await GetVideos(admin, new
            {
                FreeGmBitcoin = true
            });
        }

        public async Task<IEnumerable<Video>> GetFreeEducation(bool admin = false)
        {
            return await GetVideos(admin, new
            {
                FreeEducation = true
            });
        }

        public async Task<Video> GetVideo(int id)
        {
            var video = await DapperConnection.ExecuteGetStoredProcedureSingle<Video>("GetVideos", new
            {
                Id = id
            });

            if (video != null)
            {
                var videoDiscussions = await GetVideoDiscussions(video.Id);

                if (videoDiscussions != null)
                {
                    foreach (var videoDiscussion in videoDiscussions)
                    {
                        if (videoDiscussion.ParentId.HasValue)
                        {
                            var parentVideoDiscussion = videoDiscussions.SingleOrDefault(w => w.Id == videoDiscussion.ParentId.Value);

                            if (parentVideoDiscussion != null)
                                parentVideoDiscussion.NestedDiscussions.Add(videoDiscussion);
                        }
                        else
                            video.VideoDiscussions.Add(videoDiscussion);
                    }
                }
            }

            return video;
        }

        public async Task<Video> GetGmBitcoinVideo(int id)
        {
            var video = await DapperConnection.ExecuteGetStoredProcedureSingle<Video>("GetVideos", new
            {
                Id = id,
                FreeGmBitcoin = true
            });

            if (video != null)
            {
                var videoDiscussions = await GetVideoDiscussions(video.Id);

                if (videoDiscussions != null)
                {
                    foreach (var videoDiscussion in videoDiscussions)
                    {
                        if (videoDiscussion.ParentId.HasValue)
                        {
                            var parentVideoDiscussion = videoDiscussions.SingleOrDefault(w => w.Id == videoDiscussion.ParentId.Value);

                            if (parentVideoDiscussion != null)
                                parentVideoDiscussion.NestedDiscussions.Add(videoDiscussion);
                        }
                        else
                            video.VideoDiscussions.Add(videoDiscussion);
                    }
                }
            }

            return video;
        }

        public async Task<Video> GetFreeEducationVideo(int id)
        {
            var video = await DapperConnection.ExecuteGetStoredProcedureSingle<Video>("GetVideos", new
            {
                Id = id,
                FreeEducation = true
            });

            if (video != null)
            {
                var videoDiscussions = await GetVideoDiscussions(video.Id);

                if (videoDiscussions != null)
                {
                    foreach (var videoDiscussion in videoDiscussions)
                    {
                        if (videoDiscussion.ParentId.HasValue)
                        {
                            var parentVideoDiscussion = videoDiscussions.SingleOrDefault(w => w.Id == videoDiscussion.ParentId.Value);

                            if (parentVideoDiscussion != null)
                                parentVideoDiscussion.NestedDiscussions.Add(videoDiscussion);
                        }
                        else
                            video.VideoDiscussions.Add(videoDiscussion);
                    }
                }
            }

            return video;
        }

        public async Task<IEnumerable<VideoDiscussion>> GetVideoDiscussions(int id)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<VideoDiscussion>("GetVideoDiscussions", new
            {
                VideoId = id
            });
        }

        public async Task<VideoDiscussion> GetVideoDiscussion(int id)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<VideoDiscussion>("GetVideoDiscussions", new
            {
                Id = id
            });
        }

        public async Task<int> CreateVideoDiscussion(int videoId, string userId, string comments, int? parentId = null)
        {
            return await DapperConnection.ExecuteInsertStoredProcedureSingle("CreateVideoDiscussion",
                new
                {
                    VideoId = videoId,
                    UserId = userId,
                    Comments = comments,
                    ParentId = parentId
                });
        }
    }
}

