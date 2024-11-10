using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using System.Text.Json;

namespace Everything.Data.Repositories
{
    public interface ITypeOfApplianceRepositoryReal : IBaseRepository<TypeOfApplianceData>
    {
        IEnumerable<TypeOfApplianceData> GetTypeOfAppliancesByName(string name);
        void LoadDataFromJson(string filePath);
        void SaveDataToJson(string filePath);
        void UpdateImage(int id, string url);
        void UpdateName(int id, string newName);
    }

    public class TypeOfApplianceRepository : BaseRepository<TypeOfApplianceData>, ITypeOfApplianceRepositoryReal
    {
        public TypeOfApplianceRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public IEnumerable<TypeOfApplianceData> GetTypeOfAppliancesByName(string name)
        {
            return _dbSet
                .Where(x => x.Name.Contains(name))
                .ToList();
        }

        public void LoadDataFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                var appliances = JsonSerializer.Deserialize<List<TypeOfApplianceData>>(jsonData);

                if (appliances != null)
                {
                    foreach (var appliance in appliances)
                    {
                        appliance.Id = 0;  
                        if (!_dbSet.Any(x => x.Name == appliance.Name))
                        {
                            _dbSet.Add(appliance);
                        }
                    }
                    _webDbContext.SaveChanges();
                }
            }
        }

        public void SaveDataToJson(string filePath)
        {
            var appliances = _dbSet.ToList();
            var jsonData = JsonSerializer.Serialize(appliances, new JsonSerializerOptions { WriteIndented = true });

            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(filePath, jsonData);
        }

        public void UpdateName(int id, string newName)
        {
            var appliance = Get(id);
            if (appliance != null)
            {
                appliance.Name = newName;
                _webDbContext.SaveChanges();
            }
        }

        public void UpdateImage(int id, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL cannot be null or empty", nameof(url));
            }

            var appliance = Get(id);
            if (appliance != null)
            {
                appliance.ImageSrc = url;
                _webDbContext.SaveChanges();
            }
        }
    }
}

