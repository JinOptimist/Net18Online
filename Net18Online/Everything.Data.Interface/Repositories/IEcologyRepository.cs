using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IEcologyRepository<T> : IBaseRepository<T>
        where T : IEcologyData
    {
        //void UpdateText(int id, string newText);

        void UpdateImage(int id, string url);
        
        //void UpdatePost(int id, string url, string text));
    }
}