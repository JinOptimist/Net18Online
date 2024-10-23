namespace WebPortalEverthing.Models.LoadTesting
{
    public class LoadTestingContentMetricsListViewModel
    {
        public List<Metric> Metrics { get; set; }
        public int FridaysRemaining { get; set; }

        public LoadTestingContentMetricsListViewModel()
        {
            Metrics = new List<Metric>(); // Инициализация списка

            
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