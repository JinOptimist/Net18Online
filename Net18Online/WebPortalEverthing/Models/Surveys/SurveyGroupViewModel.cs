namespace WebPortalEverthing.Models.Surveys
{
    public class SurveyGroupViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsAllowSurveyCreation { get; set; }
        public List<SurveyViewModel> Surveys { get; set; }
        public СreatorUserViewModel? СreatorUser { get; set; }
    }
}
