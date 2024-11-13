using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Models.Surveys
{
    public class SurveyData : BaseModel, ISurveyData
    {
        public int IdStatus { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public virtual SurveyGroupData SurveyGroup { get; set; }
        public virtual List<QuestionData> Questions { get; set; } = new();
    }
}
