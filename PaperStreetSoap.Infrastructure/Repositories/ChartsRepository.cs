using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Core.Utils;
using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Core.Interfaces.Repositories;

namespace PaperStreetSoap.Infrastructure.Repositories
{
    public class ChartsRepository : BaseRepository, IChartsRepository
    {
        public ChartsRepository(IDapperContext dapperContext) : base(dapperContext)
        {
  
        }

        public async Task<IEnumerable<Chart>> GetCharts(int? take = null)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<Chart>("GetCharts", new
            {
                LiveTrading = 0,
                CountToReturn = take ?? 99999
            });
        }

        public async Task<Chart?> GetChart(int id)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<Chart>("GetCharts", new
            {
                Id = id,
                CountToReturn = 1
            });
        }

        public async Task<IEnumerable<Chart>> GetLiveTradingCharts()
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<Chart>("GetCharts", new
            {
                LiveTrading = 1
            });
        }

        public async Task<Chart?> GetLiveTradingChart()
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<Chart>("GetCharts", new
            {
                LiveTrading = 1,
                CountToReturn = 1
            });
        }
    }
}

