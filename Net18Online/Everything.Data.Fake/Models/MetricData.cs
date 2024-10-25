using Everything.Data.Interface.Models;

namespace WebPortalEverthing.Models.LoadTesting
{
    public class MetricData : IMetricData
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public decimal Throughput { get; set; }
        public decimal Average { get; set; }

        public MetricData()
        {
            Guid = Guid.NewGuid(); // Инициализация Guid при создании новой метрики
        }
    }
}
