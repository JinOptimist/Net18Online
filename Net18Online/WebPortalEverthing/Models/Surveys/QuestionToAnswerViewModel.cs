using Everything.Data.Interface.Enums;

namespace WebPortalEverthing.Models.Surveys
{
    public class QuestionToAnswerViewModel
    {
        public int AnswerId { get; set; }
        public string Title { get; set; }
        public bool IsRequired { get; set; }
        public AnswerType AnswerType { get; set; }
        public string AnswerText { get; set; }
    }
}
