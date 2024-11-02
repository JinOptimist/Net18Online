namespace Everything.Data.Models
{
    public class MetricsData
    {
        public Guid Guid { get; set; } = Guid.NewGuid(); // Инициализация Guid при создании новой метрики, запись вместо описания в конструкторе, тоже самое д.б.
        public string Name { get; set; }
        public decimal Throughput { get; set; }
        public decimal Average { get; set; }
    }
}