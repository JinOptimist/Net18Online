namespace WebPortalEverthing.Models.AnimeGirl.Profile
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
        public List<MangaShortInfoViewModel> Mangas { get; set; }
        public List<GirlShortInfoViewModel> Girls { get; set; }
    }
}
