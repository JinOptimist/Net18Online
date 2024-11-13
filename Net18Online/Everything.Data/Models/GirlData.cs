using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class GirlData : BaseModel, IGirlData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        
        public virtual UserData? Creator { get; set; }
        public virtual MangaData? Manga { get; set; }
    }
}
