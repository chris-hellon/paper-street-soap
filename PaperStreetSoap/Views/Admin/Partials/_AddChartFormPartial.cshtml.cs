using System.ComponentModel.DataAnnotations;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace PaperStreetSoap.Views.Admin.Partials
{
    public class AddChartFormModel
    {
        [Required(AllowEmptyStrings = false)]
        public string? Title { get; set; } = null;

        [Display(Name = "Image Url")]
        [RequiredIf("FileUploaded()", ErrorMessage = "Please provide an Image Url or Upload a file.")]
        public string? ImageUrl { get; set; } = null;

        [Display(Name = "Upload File")]
        [RequiredIf("ImageUrl == null", ErrorMessage = "Please provide an Image Url or Upload a file.")]
        public IFormFile? UploadedFile { get; set; } = null;

        public bool FileUploaded()
        {
            return UploadedFile != null;
        }

        public AddChartFormModel()
        {

        }
    }
}
