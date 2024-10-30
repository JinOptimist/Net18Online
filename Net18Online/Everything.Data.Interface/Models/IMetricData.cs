namespace Everything.Data.Interface.Models
{
    public interface IMetricData
    {
        Guid Guid { get; set; }
        string Name { get; set; }
        decimal Throughput { get; set; }
        decimal Average { get; set; }
    }
}
