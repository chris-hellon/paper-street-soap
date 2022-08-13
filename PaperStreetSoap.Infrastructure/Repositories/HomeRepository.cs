using ProjectBase.Infrastructure.Contexts;
using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Core.Interfaces.Repositories;
using ProjectBase.Infrastructure.Repositories;

namespace PaperStreetSoap.Infrastructure.Repositories
{
    public class HomeRepository : BaseRepository, IHomeRepository
    {
        public HomeRepository(IDapperContext dapperContext) : base(dapperContext)
        {
            
        }

        public async Task<IEnumerable<About>> GetAbouts()
        {
            return await Task.Run(() => new List<About>()
            {
                new About("fa-chart-area", "Trading", "Diam elitr kasd sed at elitr sed ipsum justo dolor sed clita amet diam. Diam elitr kasd sed at elitr sed ipsum justo dolor sed clita amet diam."),
                new About("fa-book-open", "Resources", "Diam elitr kasd sed at elitr sed ipsum justo dolor sed clita amet diam. Diam elitr kasd sed at elitr sed ipsum justo dolor sed clita amet diam."),
                new About("fa-globe", "Research", "Diam elitr kasd sed at elitr sed ipsum justo dolor sed clita amet diam. Diam elitr kasd sed at elitr sed ipsum justo dolor sed clita amet diam."),
                //new AboutModel("fa-graduation-cap", "Analysis", "Diam elitr kasd sed at elitr sed ipsum justo dolor sed clita amet diam")
            });
        }
    }
}

