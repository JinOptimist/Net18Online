using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [Required]
        public Guid Guid { get; set; } = Guid.NewGuid(); // GUID метрики, инициализируется при создании

        public double Throughput
        {
            get
            {
                return ParseInput(ThroughputInput);
            }
        }

        public double Average
        {
            get
            {
                return ParseInput(AverageInput);
            }
        }

        public int LoadVolumeId { get; set; } // Значение, которое выбрал пользователь
        public List<SelectListItem>? LoadVolumes { get; set; } // Тут будет все значения Load volumes и Id

        /// <summary>
        /// Парсит строку в число с учетом текущей локали. 
        /// Если парсинг невозможен, возвращает 0.
        /// </summary>
        private double ParseInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return 0;

            var currentCulture = CultureInfo.CurrentCulture;

            // Заменяем символы-разделители на основе текущей культуры
            if (currentCulture.NumberFormat.NumberDecimalSeparator == ",")
            {
                input = input.Replace('.', ',');
            }
            else
            {
                input = input.Replace(',', '.');
            }

            // Пытаемся распознать значение
            return double.TryParse(input, NumberStyles.Any, currentCulture, out var value)
                ? value
                : 0;
        }
    }
}
