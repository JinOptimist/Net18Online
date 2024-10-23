using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Everything.Data.Interface.Models
{
    public interface IEcologyData
    {
        int Id { get; set; }
        string ImageSrc { get; set; }
        List<string> Text { get; set; }
    }
}