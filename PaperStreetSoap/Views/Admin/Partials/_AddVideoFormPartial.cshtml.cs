using System.ComponentModel.DataAnnotations;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace PaperStreetSoap.Views.Admin.Partials
{
    public class AddVideoFormModel
    {
        [Display(Name = "Video Url")]
        [Required]
        public string? VideoUrl { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Details { get; set; }

        [Display(Name = "Market Review")]
        public bool MarketReview { get; set; } = false;

        [Display(Name = "Resources")]
        public bool Resources { get; set; } = false;

        [Display(Name = "Gm Bitcoin")]
        public bool FreeGmBitcoin { get; set; } = false;

        [Display(Name = "Education")]
        public bool FreeEducation { get; set; } = false;

        [Display(Name = "Education")]
        public bool MembersEducation { get; set; } = false;

        [Display(Name = "Latest Review")]
        public bool TodaysReview { get; set; } = false;

        public AddVideoFormModel()
        {
        }
    }
}
