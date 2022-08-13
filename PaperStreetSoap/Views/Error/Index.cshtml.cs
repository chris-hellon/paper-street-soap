namespace PaperStreetSoap.Views.Error
{
    public class IndexModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? ExceptionMessage { get; set; }
    }
}
