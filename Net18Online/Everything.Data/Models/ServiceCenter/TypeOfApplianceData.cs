using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class TypeOfApplianceData : BaseModel, ITypeOfApplianceData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
    }
}
