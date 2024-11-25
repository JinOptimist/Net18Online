using Everything.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Models
{
    public class FilmDirectorData : BaseModel, IFilmDirectorData
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public UserData? Creator { get; set; }

        public virtual List<MovieData> Movies { get; set; } = new List<MovieData>();

    }
}
