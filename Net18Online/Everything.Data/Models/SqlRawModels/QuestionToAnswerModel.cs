using Everything.Data.Interface.Enums;

namespace Everything.Data.Models.SqlRawModels
{
    public class QuestionToAnswerModel
    {
        public int AnswerId { get; set; }
        public string Title { get; set; }
        public bool IsRequired { get; set; }
        public AnswerType AnswerType { get; set; }
        public string AnswerText { get; set; }
    }
}
