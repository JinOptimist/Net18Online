using WebPortalEverthing.Models.DndSubClass;

namespace WebPortalEverthing.Models.DND
{
    public class ClassViewModel
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
        public string Name { get; internal set; }
        public List<SubClassNameAndIdViewModel> SubClasses { get; set; } = new();
    }
}
