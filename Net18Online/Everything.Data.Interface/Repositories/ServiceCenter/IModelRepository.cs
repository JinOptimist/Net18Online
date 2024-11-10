using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IModelRepository<T> : IBaseRepository<T>
        where T : IModelData
    {
        IEnumerable<T> GetModelsByProducerId(int producerId);
        IEnumerable<T> GetModelsByTypeId(int typeId);
        IEnumerable<T> GetModelsByName(string name);
        void UpdateName(int id, string newName);
        void UpdateProducer(int id, int newProducerId);
        void UpdateType(int id, int newTypeId); 
    }
}
