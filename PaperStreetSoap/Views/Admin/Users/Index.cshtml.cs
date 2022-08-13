using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Views.Admin.Partials;

namespace PaperStreetSoap.Views.Admin.Users
{
    public class IndexModel
    {
        public IEnumerable<User> Users { get; set; }

        public AddUserFormModel User { get; set; } = new AddUserFormModel();

        public IndexModel(IEnumerable<User> users)
        {
            Users = users;
        }
    }
}
