using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.ViewModels
{
    public class MembersViewModel
    {
        public Subscription? Subscription { get; set; } = null;

        public MembersViewModel(Subscription? subscription)
        {
            Subscription = subscription;
        }
    }
}

