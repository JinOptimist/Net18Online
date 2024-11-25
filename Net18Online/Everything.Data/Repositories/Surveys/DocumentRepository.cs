using Everything.Data.Interface.Repositories;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Repositories.Surveys
{
    public interface IDocumentRepositoryReal : IDocumentRepository<DocumentData>
    {
    }

    public class DocumentRepository : BaseRepository<DocumentData>, IDocumentRepositoryReal
    {
        public DocumentRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public void Create(string title, string originalFileName, string storageFileName, string contentType, long length)
        {
            var data = new DocumentData
            {
                Title = title,
                OriginalFileName = originalFileName,
                StorageFileName = storageFileName,
                ContentType = contentType,
                Length = length
            };

            _dbSet.Add(data);
            _webDbContext.SaveChanges();
        }
    }
}
