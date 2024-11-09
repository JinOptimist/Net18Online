using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class EcologyData : BaseModel, IEcologyData
    {
        public string ImageSrc { get; set; }
        public string Text { get; set; }
    }
}