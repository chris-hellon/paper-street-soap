using PaperStreetSoap.Core.Models;

namespace PaperStreetSoap.Core.Interfaces.Repositories
{
    public interface IPageContentRepository
    {
        Task<IEnumerable<PageSection>> GetPageSections(int pageId);

        Task<IEnumerable<PageContent>> GetPageSectionContent(int pageSectionId);

        Task UpdatePageSectionContent(int id, string content = null, string header = null);

        Task CreatePageSectionContent(int pageSectionId, string content, string header);
    }
}

