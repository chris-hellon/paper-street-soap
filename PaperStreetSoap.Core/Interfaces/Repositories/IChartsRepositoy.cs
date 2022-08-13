using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Core.Interfaces.Repositories
{
    public interface IChartsRepository
    {
        Task<IEnumerable<Chart>> GetCharts(int? take = null);

        Task<Chart?> GetChart(int id);

        Task<IEnumerable<Chart>> GetLiveTradingCharts();

        Task<Chart?> GetLiveTradingChart();
    }
}

