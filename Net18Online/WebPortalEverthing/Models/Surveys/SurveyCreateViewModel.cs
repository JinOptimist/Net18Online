using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.Surveys
{
    public class SurveyCreateViewModel
    {
        public SurveyGroupForListViewModel SurveyGroup { get; set; }
        public string Title { get; set; }
        [IsNullOrMinLength(20)]
        public string? Description { get; set; }
        public List<QuestionViewModel> Questions { get; set; } = new();
    }
}
