using Enums.Surveys;

namespace Everything.Data.Interface.Models.Surveys
{
    public interface ITakingUserSurveyData
    {
        DateTime StartTime { get; set; }
        DateTime? EndTime { get; set; }
        SurveyCompletionStatus CompletionStatus { get; set; }
    }
}
