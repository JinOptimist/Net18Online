namespace WebPortalEverthing.Models.Surveys
{
    public class TakingSurveyIndexViewModel
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public string SurveyTitle { get; set; }
        public List<QuestionToAnswerViewModel> Questions { get; set; }
    }
}
