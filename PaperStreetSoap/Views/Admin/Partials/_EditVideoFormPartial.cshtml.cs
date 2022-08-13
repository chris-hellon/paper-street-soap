using System.ComponentModel.DataAnnotations;

namespace PaperStreetSoap.Views.Admin.Partials
{
    public class EditVideoFormModel : AddVideoFormModel
    {
        [Required]
        public int Id { get; set; }

        public EditVideoFormModel(int id, string videoUrl, string title, string details, bool marketReview = false, bool resources = false, bool membersEducation = false, bool todaysReview = false, bool gmBitoin = false, bool freeEducation = false)
        {
            Id = id;
            Title = title;
            VideoUrl = videoUrl;
            Details = details;
            MarketReview = marketReview;
            Resources = resources;
            MembersEducation = membersEducation;
            FreeGmBitcoin = gmBitoin;
            FreeEducation = freeEducation;
            TodaysReview = todaysReview;
        }
    }
}