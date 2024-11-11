using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Interface.Models
{
    public interface IGameStudiosData : IBaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
