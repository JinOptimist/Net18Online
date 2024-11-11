using WebPortalEverthing.Models.DndSubClass;

namespace WebPortalEverthing.Models.DND
{
    public class IndexDndClassViewModel
    {
        public List<ClassViewModel> Classes { get; set; }

        public List<SubClassNameAndIdViewModel> SubClasses { get; set; }
    }
}
