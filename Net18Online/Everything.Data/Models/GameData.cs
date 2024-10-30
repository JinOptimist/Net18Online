using Everything.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Models
{
    public class GameData : BaseModel, IGameData
    {
        public string NameGame { get; set; }
        public string ImageSrc { get; set; }
        //public List<string> Tags { get; set; }
    }
}
