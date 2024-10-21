namespace WebPortalEverthing.Models.LoadTesting
{
    public class Metric
    {
        public Guid GUID { get; set; }
        public string Name { get; set; }
        public decimal Throughput { get; set; }
        public decimal Average { get; set; }
    }
}
