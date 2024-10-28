using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface ITypeOfApplianceRepository : IBaseRepository<ITypeOfApplianceData>
    {
        IEnumerable<ITypeOfApplianceData> GetTypeOfAppliancesByName(string name);
        void LoadDataFromJson(string filePath);
        void SaveDataToJson(string filePath); 
    }
}