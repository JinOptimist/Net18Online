using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Models.SqlRawModels
{
    public class MostPopularGames
    {
        public string GameTitle { get; set; }
        public int PlayersCount { get; set; }
        public int AverageAge { get; set; }
    }
}
