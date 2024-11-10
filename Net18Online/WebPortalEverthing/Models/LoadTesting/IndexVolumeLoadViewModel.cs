using WebPortalEverthing.Models.AnimeGirl;
using WebPortalEverthing.Models.Manga;

namespace WebPortalEverthing.Models.LoadTesting
{
    public class IndexVolumeLoadViewModel
    {
        public List<LoadVolumeShortInfoViewModel> LoadVolumes { get; set; }

        public List<MetricNameAndIdViewModel> Metrics { get; set; }
    }
}
