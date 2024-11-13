using Everything.Data.Interface.Enums;

namespace WebPortalEverthing.Models.Surveys
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsRequired { get; set; }
        public AnswerType AnswerType { get; set; }
    }
}
