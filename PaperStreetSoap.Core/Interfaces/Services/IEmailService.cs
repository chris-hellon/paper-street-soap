namespace PaperStreetSoap.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task Send(string to, string subject, string html, string? displayName = null, string? from = null);
    }
}

