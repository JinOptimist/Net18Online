using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Models
{
    public class PaginatedMessages
    {
        public List<string> Messages { get; set; }
        public int TotalCount { get; set; }
    }
}
