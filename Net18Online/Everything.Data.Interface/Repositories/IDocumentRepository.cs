using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Interface.Repositories
{
    public interface IDocumentRepository<T> : IBaseRepository<T>
        where T : IDocumentData
    {
        void Create(string title, string originalFileName, string storageFileName, string contentType, long length);
    }
}
