using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebPortalEverthing.Models.LoadTesting.TestingAttributes;

namespace WebPortalEverthing.Models.LoadTesting
{
    public class MetricCreationViewModel
    {
    //    [UniqMetricId]
        public int Id { get; set; }

        [Required]
        [StringLength(100), MinLength(3,ErrorMessage ="Name must be between 3 and 100 characters")]
        [UniqMetricName]
        public string Name { get; set; }

        [Required]
        Guid Guid { get; set; } = Guid.NewGuid(); // Инициализация Guid при создании новой метрики, вместо описания в конструкторе

        [Required]
        [ZeroUpAttribute]
        public decimal Throughput { get; set; }

        [Required]
        [ZeroUpAttribute]
        [IsCorrectAverageAttribute]
        public decimal Average { get; set; }
    }
}
