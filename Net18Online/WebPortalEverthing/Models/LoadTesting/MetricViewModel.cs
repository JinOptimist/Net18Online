namespace WebPortalEverthing.Models.LoadTesting
{
    public class MetricViewModel
    {
        public int Id { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid(); // Инициализация Guid при создании новой метрики, вместо описания в конструкторе
        public string Name { get; set; }
        public decimal Throughput { get; set; }
        public decimal Average { get; set; }
    }
}
