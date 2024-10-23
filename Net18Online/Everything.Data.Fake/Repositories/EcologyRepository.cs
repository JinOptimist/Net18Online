using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class EcologyRepository : IEcologyRepository
    {
        private List<IEcologyData> ecology = new List<IEcologyData>();

        void IEcologyRepository.Add(IEcologyData data)
        {
           data.Id = ecology.Any()
                ? ecology.Max(x => x.Id) + 1
                : 1;

            ecology.Add(data);
        }

        bool IEcologyRepository.Any()
        {
            return ecology.Any();
        }

        void IEcologyRepository.Delete(IEcologyData data)
        {
            ecology.Remove(data);
        }

        public IEcologyData? Get(int id)
        {
            return ecology.FirstOrDefault(x => x.Id == id);
        }

        public List<IEcologyData> GetAll()
        {
            return ecology;
        }
    }
}