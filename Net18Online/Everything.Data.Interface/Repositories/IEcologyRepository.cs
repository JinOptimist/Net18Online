using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IEcologyRepository<T> : IBaseRepository<T>
        where T : IEcologyData
    {   
        public void UpdatePost(int id, string url, string text);
    }
}