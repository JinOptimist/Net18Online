using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IClientRepository<T> : IBaseRepository<T>
       where T : IClientData
    {
        void UpdateBalance(int id, decimal newBalance);
        void UpdatePhoneNumber(int id, string phoneNumber);
    }
}
