using WebPortalEverthing.Models.AnimeGirl.Profile;

namespace WebPortalEverthing.Models.LoadTesting.Profile
{
    public class LoadUserProfileViewModel
    {
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public List<LoadVolumeShortInfoViewModel> LoadValumes { get; set; }
        public List<MetricShortInfoViewModel> Metrics { get; set; }
    }

}
