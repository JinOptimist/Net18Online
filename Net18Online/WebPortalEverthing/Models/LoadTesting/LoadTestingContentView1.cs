﻿namespace WebPortalEverthing.Models.LoadTesting
{
    public class LoadTestingContentView1
    {
        public List<Metric> Metrics { get; set; }

        public LoadTestingContentView1()
        {
            Metrics = new List<Metric>(); // Инициализация списка

            for (int i = 1; i <= 10; i++)
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