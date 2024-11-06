using Everything.Data.Interface.Models;

namespace Everything.Data.Interface.Repositories
{
    public interface ITypeOfApplianceRepository<T> : IBaseRepository<T>
        where T : ITypeOfApplianceData
    {
        IEnumerable<T> GetTypeOfAppliancesByName(string name);
        void LoadDataFromJson(string filePath);
        void SaveDataToJson(string filePath);
        void UpdateName(int id, string newName);
        void UpdateImage(int id, string url);
    }
}