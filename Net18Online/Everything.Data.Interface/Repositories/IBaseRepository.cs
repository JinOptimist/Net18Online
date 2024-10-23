namespace Everything.Data.Interface.Repositories
{
    public interface IBaseRepository<T>
    {
        void Add(T data);

        void Delete(T data);

        List<T> GetAll();

        T? Get(int id);

        bool Any();
    }
}
