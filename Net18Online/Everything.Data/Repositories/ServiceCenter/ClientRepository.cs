using Everything.Data.Interface.Repositories;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public class ClientRepository : IClientRepository<ClientData>
    {
        private readonly WebDbContext _webDbContext;

        public ClientRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public int Add(ClientData data)
        {
            _webDbContext.Clients.Add(data);
            _webDbContext.SaveChanges();

            return data.Id;
        }

        public bool Any()
        {
            return _webDbContext.Clients.Any();
        }

        public void Delete(ClientData data)
        {
            _webDbContext.Clients.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            if (data != null)
            {
                Delete(data);
            }
        }

        public ClientData? Get(int id)
        {
            return _webDbContext.Clients.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ClientData> GetAll()
        {
            return _webDbContext.Clients.ToList();
        }

        public void UpdateBalance(int id, decimal newBalance)
        {
            var client = Get(id);
            if (client != null)
            {
                client.Balance = newBalance;
                _webDbContext.SaveChanges();
            }
        }

        public void UpdatePhoneNumber(int id, string phoneNumber)
        {
            var client = Get(id);
            if (client != null)
            {
                client.PhoneNumber = phoneNumber;
                _webDbContext.SaveChanges();
            }
        }
    }
}
