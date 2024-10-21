namespace WebPortalEverthing.Models.LoadTesting
{
    public class LoadTestingContentViewModel1
    {
        public List<Metric> Metrics { get; set; }
        public int FridaysRemaining { get; set; }

        public LoadTestingContentViewModel1(int countMetrics)
        {
            Metrics = new List<Metric>(); // Инициализация списка

            for (int i = 1; i <= countMetrics; i++)
            {
                Metrics.Add(new Metric
                {
                    GUID = Guid.NewGuid(),
                    Name = $"Metric {i}",
                    Throughput = i * 10.5m,
                    Average = i * 5.0m
                });
            }


            // Текущая дата
            DateTime today = DateTime.Now;

            // Дата наступления Нового Года
            DateTime newYear = new DateTime(today.Year, 12, 31);

            // Количество пятниц до Нового Года
            FridaysRemaining = 0;

            // Проход по дням до Нового Года
            DateTime currentDay = today;
            while (currentDay < newYear)
            {
                if (currentDay.DayOfWeek == DayOfWeek.Friday)
                {
                    FridaysRemaining++;
                }
                currentDay = currentDay.AddDays(1);
            }
        }
    }
}