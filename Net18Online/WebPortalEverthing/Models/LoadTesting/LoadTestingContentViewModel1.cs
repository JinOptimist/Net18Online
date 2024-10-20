namespace WebPortalEverthing.Models.LoadTesting
{
    public class LoadTestingContentViewModel1
    {
        public List<Metric> Metrics { get; set; }

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
        }
    }
}