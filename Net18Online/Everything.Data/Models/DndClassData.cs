using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class DndClassData : BaseModel, IDNDData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
    }
}
