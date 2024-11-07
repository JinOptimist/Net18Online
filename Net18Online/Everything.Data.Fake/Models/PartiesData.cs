using Everything.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Fake.Models
{
    public class PartiesData: IPartiesData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get ; set; }
        public string Winner { get; set; }
    }
}
