namespace PaperStreetSoap.Views.Admin.Partials
{
    public class DeleteRecordFormModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Handler { get; set; }
        public string? UserId { get; set; }

        public DeleteRecordFormModel(string title, string handler, int? id = null, string? userId = null)
        {
            Id = id;
            Title = title;
            Handler = handler;
            UserId = userId;
        }
    }
}
