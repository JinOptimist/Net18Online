using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IEcologyRepositoryReal : IEcologyRepository<EcologyData>
    {
    }

    public class EcologyRepository : IEcologyRepositoryReal
    {
        private WebDbContext _webDbContext;

        public EcologyRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }
        
        public void Add(EcologyData data)
        {
            _webDbContext.Add(data);
            _webDbContext.SaveChanges();
        }

        public bool Any()
        {
            return _webDbContext.Ecologies.Any();
        }

        public void Delete(EcologyData data)
        {
            _webDbContext.Ecologies.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            Delete(data);
        }

        public EcologyData? Get(int id)
        {
            return _webDbContext.Ecologies.FirstOrDefault(x => x.Id == id);
        }
        
        public IEnumerable<EcologyData> GetAll()
        {
            return _webDbContext
                    .Ecologies
                    .Where(x => !string.IsNullOrEmpty(x.ImageSrc))
                    .ToList();
        }

        public void UpdateImage(int id, string url)
        {
            var ecology = _webDbContext.Ecologies.First(x => x.Id == id);

            ecology.ImageSrc = url;

            _webDbContext.SaveChanges();
        }

        /*public void UpdatePost(int id, string url, string text)
        { 
            var ecology = _ecologies.FirstOrDefault(e => e.Id == id); 
            if (ecology != null) 
            { 
                ecology.ImageSrc = url;
                ecology.Texts = new List<string> { text }; 
            } 
        }*/
    }
}    