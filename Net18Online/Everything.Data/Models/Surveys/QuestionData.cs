using Everything.Data.Interface.Enums;
using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Models.Surveys
{
    public class QuestionData : BaseModel, IQuestionData
    {
        public string Title { get; set; }
        public bool IsRequired { get; set; }
        public AnswerType AnswerType { get; set; }
        public virtual SurveyData Survey { get; set; }
    }
}