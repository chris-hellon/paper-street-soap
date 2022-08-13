using PaperStreetSoap.Core.Models;
namespace PaperStreetSoap.Core.Interfaces.Repositories
{
    public interface IPackagesRepository
    {
        Task<IEnumerable<Package>> GetPackages();

        Task<Package> GetPackage(int id);
    }
}

