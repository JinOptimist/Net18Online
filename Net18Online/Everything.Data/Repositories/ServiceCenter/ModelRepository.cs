using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public class ModelRepository : BaseRepository<ModelData>, IModelRepository<ModelData>
    {
        public ModelRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<ModelData> GetModelsByProducerId(int producerId)
        {
            return _dbSet.
                Where(x => x.ProducerId == producerId).
                ToList();
        }

        public IEnumerable<ModelData> GetModelsByTypeId(int typeId)
        {
            return _dbSet.
                Where(x => x.TypeId == typeId).
                ToList();
        }

        public IEnumerable<ModelData> GetModelsByName(string name)
        {
            return _dbSet.
                Where(x => x.Name.Contains(name)).
                ToList();
        }

        public void UpdateName(int id, string newName)
        {
            var model = Get(id);
            if (model != null)
            {
                model.Name = newName;
                _webDbContext.SaveChanges();
            }
        }

        public void UpdateProducer(int id, int newProducerId)
        {
            var model = Get(id);
            if (model != null)
            {
                model.ProducerId = newProducerId;
                _webDbContext.SaveChanges();
            }
        }

        public void UpdateType(int id, int newTypeId)
        {
            var model = Get(id);
            if (model != null)
            {
                model.TypeId = newTypeId;
                _webDbContext.SaveChanges();
            }
        }
    }
}
