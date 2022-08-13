using System.ComponentModel.DataAnnotations;

namespace PaperStreetSoap.Views.Admin.Partials
{
    public class AddPackageFormModel
    {
        [Required(AllowEmptyStrings = false)]
        public string? Title { get; set; } = null;

        public string? Description { get; set; } = null;

        [Required]
        [DisplayFormat(DataFormatString = "{0:G29}", ApplyFormatInEditMode = true)]
        public decimal? Price { get; set; }

        [Required]
        public int Duration { get; set; }

        public string DurationType { get; set; }

        //[Required]
        //[Display(Name = "Open Node Key")]
        //public string OpenNodeKey { get; set; }

        public AddPackageFormModel()
        {

        }
    }
}
