using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IProducerRepository<T> : IBaseRepository<T>
        where T : IProducerData
    {
        IEnumerable<T> GetProducersByName(string name);
        void UpdateProducerName(int id, string newProducerName);
        bool HasModels(int producerId);
    }
}
