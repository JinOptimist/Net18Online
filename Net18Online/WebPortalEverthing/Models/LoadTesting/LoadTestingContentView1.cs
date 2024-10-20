namespace WebPortalEverthing.Models.LoadTesting
{
    public class LoadTestingContentView1
    {
        public List<Metric> Metrics { get; set; }

        // Конструктор класса, инициализирующий список метрик
        public LoadTestingContentView1()
        {
            Metrics = new List<Metric>();

            for (int i = 1; i <= 10; i++)
            {
                Metrics.Add(new Metric
                {
                    GUID = Guid.NewGuid(),
                    Name = $"Metric {i}",
                    Throughput = i * 10.5m, // Пример значений, изменяемых в зависимости от итерации
                    Average = i * 5.0m
                });
            }
        }

    }
}