using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IProducerRepositoryReal : IBaseRepository<ProducerData>
    {
        IEnumerable<ProducerData> GetProducersByName(string name);
        void UpdateProducerName(int id, string newProducerName);
        bool HasModels(int producerId);
    }

    public class ProducerRepository : BaseRepository<ProducerData>, IProducerRepositoryReal
    {
        public ProducerRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<ProducerData> GetProducersByName(string name)
        {
            return _dbSet.
                Where(x => x.Producer.Contains(name)).
                ToList();
        }

        public void UpdateProducerName(int id, string newProducerName)
        {
            var producer = Get(id);
            if (producer != null)
            {
                producer.Producer = newProducerName;
                _webDbContext.SaveChanges();
            }
        }

        public bool HasModels(int producerId)
        {
            return _dbSet.Any(x => x.Id == producerId && x.ModelsOnProducer.Any());
        }
    }
}
