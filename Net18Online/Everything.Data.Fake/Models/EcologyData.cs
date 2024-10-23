using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Everything.Data.Interface.Models;

namespace Everything.Data.Fake.Models
{
    public class EcologyData : IEcologyData
    {
         public string ImageSrc { get; set; }
        public List<string> Text { get; set; }
        public int Id { get; set; }
    }
}