using System.ComponentModel.DataAnnotations;

namespace PaperStreetSoap.Views.Shared
{
    public class VideoDiscussionAddCommentModel
    {
        public int VideoId { get; set; }
        public string ProfileImageUrl { get; set; } = string.Empty;
        [Required(AllowEmptyStrings = false)]
        public string? Comment { get; set; } = null;

        public int? ParentId { get; set; }

        public VideoDiscussionAddCommentModel()
        {

        }
        public VideoDiscussionAddCommentModel(int videoId, string profileImageUrl, int? parentId = null)
        {
            VideoId = videoId;
            ProfileImageUrl = profileImageUrl;
            ParentId = parentId;
        }
    }
}
