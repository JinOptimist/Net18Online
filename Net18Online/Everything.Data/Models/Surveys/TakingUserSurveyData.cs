using Enums.Surveys;
using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Models.Surveys
{
    public class TakingUserSurveyData : BaseModel, ITakingUserSurveyData
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public SurveyCompletionStatus CompletionStatus { get; set; }
        public virtual UserData User { get; set; }
        public virtual SurveyData Survey { get; set; }
        public virtual List<AnswerToQuestionData> AnswerToQuestions { get; set; }
    }
}