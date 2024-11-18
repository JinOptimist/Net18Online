using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class AnimeData : BaseModel, IAnimeCatalogData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public virtual List<AnimeReviewData> Reviews { get; set; } = new List<AnimeReviewData>();
    }
}
