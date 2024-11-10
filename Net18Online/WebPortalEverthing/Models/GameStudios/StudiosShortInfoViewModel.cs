using WebPortalEverthing.Models.GameStore;

namespace WebPortalEverthing.Models.GameStudios
{
    public class StudiosShortInfoViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<GameShortInfoViewModel> GameNames { get; set; } = new List<GameShortInfoViewModel>();
    }
}
