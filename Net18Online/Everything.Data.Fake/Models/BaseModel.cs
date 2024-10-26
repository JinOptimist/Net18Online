using Everything.Data.Interface.Models;

namespace Everything.Data.Fake.Models
{
    public abstract class BaseModel : IBaseModel
    {
        public int Id { get; set; }
    }
}
