using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface ICakeRepository
    {
        void Add(ICakeData data);

        void Delete(ICakeData data);

        List<ICakeData> GetAll();

        ICakeData? Get(int id);

        bool Any();
    }
}
