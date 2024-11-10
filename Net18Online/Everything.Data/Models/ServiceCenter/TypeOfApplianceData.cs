using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class TypeOfApplianceData : BaseModel, ITypeOfApplianceData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }

        public virtual List<ModelData> ModelsOnType { get; set; } = new List<ModelData>();
    }
}
