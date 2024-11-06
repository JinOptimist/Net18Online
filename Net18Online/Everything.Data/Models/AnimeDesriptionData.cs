using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class AnimeDescriptionData : BaseModel, IAnimeDescriptionData
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<AnimeData> Animes { get; set; } = new List<AnimeData>();
    }
}
