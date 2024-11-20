using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class TypeOfApplianceData : BaseModel, ITypeOfApplianceData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }

        public virtual ICollection<ModelData> ModelsOnType { get; set; } = new List<ModelData>();
    }
}
