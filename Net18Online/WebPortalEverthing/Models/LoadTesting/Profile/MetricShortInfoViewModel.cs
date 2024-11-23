namespace WebPortalEverthing.Models.LoadTesting.Profile
{
    public class MetricShortInfoViewModel
    {
        public string Name { get; set; }
        public decimal Throughput { get; set; }
        public decimal Average { get; set; }

        public string LoadVolumeName { get; set; }
    }
}
