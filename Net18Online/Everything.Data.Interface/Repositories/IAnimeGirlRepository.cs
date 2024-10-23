using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IAnimeGirlRepository
    {
        void Add(IGirlData data);

        void Delete(IGirlData data);

        List<IGirlData> GetAll();

        IGirlData? Get(int id);

        bool Any();
    }
}
