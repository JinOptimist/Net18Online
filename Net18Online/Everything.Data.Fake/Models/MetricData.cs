using Everything.Data.Fake.Models;
using Everything.Data.Interface.Models;

namespace WebPortalEverthing.Models.LoadTesting
{
    public class MetricData : BaseModel, IMetricData
    {
        public Guid Guid { get; set; } = Guid.NewGuid(); // Инициализация Guid при создании новой метрики, запись вместо описания в конструкторе, тоже самое д.б.
        public string Name { get; set; }
        public decimal Throughput { get; set; }
        public decimal Average { get; set; }
    }
}
