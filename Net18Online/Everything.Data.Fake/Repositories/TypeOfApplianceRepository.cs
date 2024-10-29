using Everything.Data.Fake.Models;
using Everything.Data.Interface.Models;
using Everything.Data.Interface.Repositories;
using System.Text.Json;

namespace Everything.Data.Fake.Repositories
{
    public class TypeOfApplianceRepository : ITypeOfApplianceRepository
    {
        private readonly List<ITypeOfApplianceData> _typeOfAppliances = new();

        public void Add(ITypeOfApplianceData data)
        {
            data.Id = _typeOfAppliances.Any() ? _typeOfAppliances.Max(x => x.Id) + 1 : 1;
            _typeOfAppliances.Add(data);
        }

        public void Delete(ITypeOfApplianceData data)
        {
            _typeOfAppliances.Remove(data);
        }

        public List<ITypeOfApplianceData> GetAll()
        {
            return _typeOfAppliances;
        }

        public ITypeOfApplianceData? Get(int id)
        {
            return _typeOfAppliances.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ITypeOfApplianceData> GetTypeOfAppliancesByName(string name)
        {
            return _typeOfAppliances.Where(appliance => appliance.Name.Contains(name)).ToList();
        }

        public bool Any()
        {
            return _typeOfAppliances.Any();
        }

        #region JSON
        /// <summary>
        /// Method to load data from JSON file
        /// </summary>
        public void LoadDataFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                var jsonString = File.ReadAllText(filePath);
                var appliances = JsonSerializer.Deserialize<List<TypeOfApplianceData>>(jsonString);

                if (appliances != null)
                {
                    foreach (var appliance in appliances)
                    {
                        Add(appliance); // Use the Add method to add the appliance
                    }
                }
                else
                {
                    // Log or debug: Deserialization failed
                    Console.WriteLine("Deserialization returned null.");
                }
            }
            else
            {
                // Log or debug: File not found
                Console.WriteLine("File not found: " + filePath);
            }
        }

        public void SaveDataToJson(string filePath)
        {
            var jsonString = JsonSerializer.Serialize(_typeOfAppliances);
            File.WriteAllText(filePath, jsonString);
        }
        #endregion
    }
}
