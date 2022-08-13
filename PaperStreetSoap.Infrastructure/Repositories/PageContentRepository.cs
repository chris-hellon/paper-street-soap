using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Repositories;
using ProjectBase.Core.Utils;
using PaperStreetSoap.Core.Models;
using PaperStreetSoap.Core.Interfaces.Repositories;

namespace PaperStreetSoap.Infrastructure.Repositories
{
    public class PageContentRepository : BaseRepository, IPageContentRepository
    {
        public PageContentRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async Task<IEnumerable<PageSection>> GetPageSections(int pageId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<PageSection>("GetPageSections", new
            {
                PageId = pageId
            });
        }

        public async Task<IEnumerable<PageContent>> GetPageSectionContent(int pageSectionId)
        {
            return await DapperConnection.ExecuteGetStoredProcedureList<PageContent>("GetPageSectionContent", new
            {
                PageSectionId = pageSectionId
            });
        }

        public async Task UpdatePageSectionContent(int id, string content, string header)
        {
            await DapperConnection.ExecuteUpdateStoredProcedureSingle("UpdatePageSectionContent", new
            {
                Id = id,
                Content = content ?? "",
                Header = header ?? ""
            });
        }

        public async Task CreatePageSectionContent(int pageSectionId, string content, string header)
        {
            await DapperConnection.ExecuteInsertStoredProcedureSingle("CreatePageSectionContent", new {
                PageSectionId = pageSectionId,
                Content = content ?? "",
                Header = header ?? ""
            });
        }
    }
}

