namespace WebPortalEverthing.Models.LoadTesting
{
    public class _LayoutLoadTestingModel
    {
        public int FridaysRemaining { get; set; }

        public _LayoutLoadTestingModel()
        {
            // Текущая дата
            DateTime today = DateTime.Now;

            // Дата наступления Нового Года
            DateTime newYear = new DateTime(today.Year + 1, 1, 1);

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
