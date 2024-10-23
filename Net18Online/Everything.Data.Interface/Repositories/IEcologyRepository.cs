using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface IEcologyRepository
    {
        void Add(IEcologyData data);

        void Delete(IEcologyData data);

        List<IEcologyData> GetAll();

        IEcologyData? Get(int id);

        bool Any();
    }
}