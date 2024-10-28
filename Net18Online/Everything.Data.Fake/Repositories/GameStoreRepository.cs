using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Fake.Repositories
{
    public class GameStoreRepository : BaseRepository<IGameData>, IGameStoreRepository
    {
    }
}
