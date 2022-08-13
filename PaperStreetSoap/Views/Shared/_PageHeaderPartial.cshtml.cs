namespace PaperStreetSoap.Views.Shared
{
    public class PageHeaderPartialModel 
    {
        public bool ShowUserName { get; set; } = false;
        public bool ShowBackButton { get; set; } = false;

        public PageHeaderPartialModel()
        {

        }

        public PageHeaderPartialModel(bool showUserName = false, bool showBackButton = false)
        {
            ShowUserName = showUserName;
            ShowBackButton = showBackButton;
        }
    }
}
