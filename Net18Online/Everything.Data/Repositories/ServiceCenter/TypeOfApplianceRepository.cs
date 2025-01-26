using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using System.Text.Json;

namespace Everything.Data.Repositories
{
    public interface ITypeOfApplianceRepositoryReal : ITypeOfApplianceRepository<TypeOfApplianceData>
    {
    }

    public class TypeOfApplianceRepository : ITypeOfApplianceRepositoryReal
    {
        private readonly WebDbContext _webDbContext;

        public TypeOfApplianceRepository(WebDbContext webDbContext)
        {
            _webDbContext = webDbContext;
        }

        public int Add(TypeOfApplianceData data)
        {
            _webDbContext.TypeOfAppliances.Add(data);
            _webDbContext.SaveChanges();

            return data.Id;
        }

        public bool Any()
        {
            return _webDbContext.TypeOfAppliances.Any();
        }

        public void Delete(TypeOfApplianceData data)
        {
            _webDbContext.TypeOfAppliances.Remove(data);
            _webDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = Get(id);
            if (data != null)
            {
                Delete(data);
            }
        }

        public TypeOfApplianceData? Get(int id)
        {
            return _webDbContext.TypeOfAppliances.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<TypeOfApplianceData> GetAll()
        {
            return _webDbContext.TypeOfAppliances.ToList();
        }

        public IEnumerable<TypeOfApplianceData> GetTypeOfAppliancesByName(string name)
        {
            return _webDbContext.TypeOfAppliances
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

                        if (!_webDbContext.TypeOfAppliances.Any(x => x.Name == appliance.Name))
                        {
                            _webDbContext.TypeOfAppliances.Add(appliance);
                        }
                    }
                    _webDbContext.SaveChanges();
                }
            }
        }

        public void SaveDataToJson(string filePath)
        {
            var appliances = _webDbContext.TypeOfAppliances.ToList();
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

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}
