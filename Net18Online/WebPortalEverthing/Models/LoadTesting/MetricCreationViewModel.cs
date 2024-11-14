using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.LoadTesting.TestingAttributes;

namespace WebPortalEverthing.Models.LoadTesting
{
    public class MetricCreationViewModel
    {
        //    [UniqMetricId]
        public int Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3, ErrorMessage = "Name must be between 3 and 100 characters")]
        [UniqMetricName]
        public string Name { get; set; }

        [Required]
        public Guid Guid { get; set; } = Guid.NewGuid(); // Инициализация Guid при создании новой метрики, вместо описания в конструкторе

        [Required]
        [ZeroUpAttribute]
        public decimal Throughput { get; set; }

        [Required]
        [ZeroUpAttribute]
        [IsCorrectAverage(0.01, 10000.00, UnitLoad.Seconds, LoadLevel.Medium)]
        public decimal Average { get; set; }
    }
}
