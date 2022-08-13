using System.ComponentModel.DataAnnotations;

namespace PaperStreetSoap.Views.Admin.Partials
{
    public class AddUserFormModel
    {
        [Required(AllowEmptyStrings = false)]
        public string? Username { get; set; } = null;

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string? Email { get; set; } = null;

        [Required(AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; } = null;

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; } = null;

        [Display(Name = "Package")]
        public int? PackageId { get; set; } = null;

        public AddUserFormModel()
        {

        }
    }
}
