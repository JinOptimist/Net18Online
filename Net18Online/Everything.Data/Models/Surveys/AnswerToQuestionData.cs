using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Models.Surveys
{
    public class AnswerToQuestionData : BaseModel, IAnswerToQuestionData
    {
        public string Text { get; set; }
        public virtual TakingUserSurveyData TakingUserSurvey { get; set; }
        public virtual QuestionData Question { get; set; }
    }
}
