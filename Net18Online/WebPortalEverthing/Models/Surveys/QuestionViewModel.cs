using Everything.Data.Interface.Enums;

namespace WebPortalEverthing.Models.Surveys
{
    public class QuestionViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public bool IsRequired { get; set; }
        public AnswerType AnswerType { get; set; }
    }
}
