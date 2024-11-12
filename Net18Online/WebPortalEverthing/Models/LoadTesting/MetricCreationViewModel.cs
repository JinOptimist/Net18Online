using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.LoadTesting
{
    public class MetricCreationViewModel
    {
    //    [UniqMetricId]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
    //    [UniqueMetricName]
        public string Name { get; set; }

        [Required]
        Guid Guid { get; set; } = Guid.NewGuid(); // Инициализация Guid при создании новой метрики, вместо описания в конструкторе

        [Required]
        public decimal Throughput { get; set; }

        [Required]
        public decimal Average { get; set; }
    }
}
