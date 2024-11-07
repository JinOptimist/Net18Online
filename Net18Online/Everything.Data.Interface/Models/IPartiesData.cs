using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Interface.Models
{
    public interface IPartiesData
    {
        int Id { get; set; }
        String Name { get; set; }

        String Color { get; set; }

        String Winner { get; set; }
    }
}
