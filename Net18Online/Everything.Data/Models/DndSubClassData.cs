using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class DndSubClassData : BaseModel, IDndSubClassData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public virtual DndClassData? DndClass { get; set; }
    }
}
