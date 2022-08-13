using System.ComponentModel.DataAnnotations;

namespace PaperStreetSoap.Views.Admin.Partials
{
    public class EditPackageFormModel : AddPackageFormModel
    {
        [Required]
        public int Id { get; set; }

        public EditPackageFormModel(int id, string title, decimal price, int duration, string durationType, string? description = null)
        {
            Id = id;
            Title = title;
            Price = price;
            Description = description;
            Duration = duration;
            DurationType = durationType;
            //OpenNodeKey = openNodeKey;
        }

        public EditPackageFormModel()
        {

        }
    }
}
