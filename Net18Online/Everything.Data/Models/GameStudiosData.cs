using Everything.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Models
{
    public class GameStudiosData : BaseModel, IGameStudiosData
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<GameData> Games { get; set; } = new List<GameData>();
    }
}
