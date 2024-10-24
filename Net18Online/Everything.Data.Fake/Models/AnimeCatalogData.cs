using Everything.Data.Interface.Models;

namespace Everything.Data.Fake.Models
{
    public class AnimeCatalogData : IAnimeCatalogData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
    }
}
