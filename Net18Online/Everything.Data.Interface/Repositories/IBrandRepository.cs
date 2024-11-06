using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IBrandRepository<T> : IBaseRepository<T>
        where T : IBrandData
    {
        public IEnumerable<T> GetAllWithCoffe();

        void LinkCoffe(int brandId, int coffeId);
    }
}
