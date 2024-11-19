using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace WebPortalEverthing.Models.LoadTesting
{
    public class MetricCreationViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Required]
        public string ThroughputInput { get; set; } // Получение из формы в строковом виде

        [Required]
        public string AverageInput { get; set; } // Получение из формы в строковом виде

        // GUID метрики, инициализируется при создании
        [Required]
        public Guid Guid { get; set; } = Guid.NewGuid();

        public double Throughput
        {
            get
            {
                return double.TryParse(ThroughputInput.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var value)
                    ? value
                    : 0;
            }
        }

        public double Average
        {
            get
            {
                return double.TryParse(AverageInput.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var value)
                    ? value
                    : 0;
            }
        }
    }
}
