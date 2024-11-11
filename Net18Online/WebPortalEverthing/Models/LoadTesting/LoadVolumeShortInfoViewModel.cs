
namespace WebPortalEverthing.Models.LoadTesting
{
    public class LoadVolumeShortInfoViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<MetricNameAndIdViewModel> Metrics { get; set; } = new();
    }
}
