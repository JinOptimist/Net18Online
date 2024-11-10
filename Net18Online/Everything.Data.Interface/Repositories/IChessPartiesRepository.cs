using Everything.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Interface.Repositories
{
    public interface IChessPartiesRepository
    {
        void Add(IPartiesData party);

        void Delete(IPartiesData party);

        List<IPartiesData> GetAll();

        IPartiesData? Get(int id);
    }
}
