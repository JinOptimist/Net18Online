using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Interface.Models
{
    public interface IGameData : IBaseModel
    {
        string NameGame { get; set; }
        string ImageSrc { get; set; }
        //List<string> Tags { get; set; }
    }
}
