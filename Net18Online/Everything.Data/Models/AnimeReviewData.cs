using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class AnimeReviewData : BaseModel, IAnimeReviewData
    {
        public string Review { get; set; }
        public virtual UserData? Creator { get; set; }
        public virtual AnimeData? Anime { get; set; }
    }
}
