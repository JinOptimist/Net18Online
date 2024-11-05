using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface ICakeRepository<T> : IBaseRepository<T>
        where T : ICakeData
    {
        void UpdateDescription(int id, string newDescription);
    }
}
