using Everything.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Interface.Repositories
{
    public interface IGameStudiosRepository<T> : IBaseRepository<T> 
        where T : IGameStudiosData
    {
    }
}
