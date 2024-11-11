using Everything.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Interface.Repositories
{
    public interface IGameStoreRepository<T> : IBaseRepository<T>
        where T : IGameData
    {
        void UpdateName(int id, string newName);

        void UpdateImage(int id, string url);
    }
}
