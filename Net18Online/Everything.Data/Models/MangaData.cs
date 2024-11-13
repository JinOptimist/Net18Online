using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class MangaData : BaseModel, IMangaData
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public UserData? Author { get; set; }
        public virtual List<GirlData> Characters { get; set; } = new List<GirlData>();
    }
}
