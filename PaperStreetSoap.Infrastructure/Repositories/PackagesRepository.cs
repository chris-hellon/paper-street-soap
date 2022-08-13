using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Core.Utils;
using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Core.Interfaces.Repositories;

namespace PaperStreetSoap.Infrastructure.Repositories
{
    public class PackagesRepository : BaseRepository, IPackagesRepository
    {
        public PackagesRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

        public async Task<IEnumerable<Package>> GetPackages()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<Package>("GetPackages");
        }

        public async Task<Package> GetPackage(int id)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<Package>("GetPackages", new
            {
                Id = id
            });
        }
    }
}

