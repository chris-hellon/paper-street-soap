using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Core.Interfaces.Repositories
{
    public interface IHomeRepository
    {
        Task<IEnumerable<About>> GetAbouts();
    }
}

