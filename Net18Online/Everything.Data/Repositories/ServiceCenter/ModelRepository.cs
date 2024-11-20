using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Everything.Data.Repositories
{
    public interface IModelRepositoryReal : IBaseRepository<ModelData>
    {
        IEnumerable<ModelData> GetModelsByProducerId(int producerId);
        IEnumerable<ModelData> GetModelsByTypeId(int typeId);
        IEnumerable<ModelData> GetModelsByName(string name);
        IEnumerable<ModelData> GetAllModels();
        void UpdateName(int id, string newName);
        void UpdateProducer(int id, int newProducerId);
        void UpdateType(int id, int newTypeId);
    }

    public class ModelRepository : BaseRepository<ModelData>, IModelRepositoryReal
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

        public IEnumerable<ModelData> GetAllModels()
        {
            return _dbSet.Include(m => m.ModelProducer)
                         .Include(m => m.ModelType)
                         .ToList();
        }
    }
}
