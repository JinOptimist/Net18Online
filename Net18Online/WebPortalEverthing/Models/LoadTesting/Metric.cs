namespace WebPortalEverthing.Models.LoadTesting
{
    public class Metric
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public decimal Throughput { get; set; }
        public decimal Average { get; set; }

        public Metric()
        {
            Guid = Guid.NewGuid(); // Инициализация Guid при создании новой метрики
        }
    }
}
