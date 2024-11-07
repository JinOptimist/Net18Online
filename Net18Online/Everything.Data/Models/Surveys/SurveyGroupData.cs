using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Models.Surveys
{
    public class SurveyGroupData : BaseModel, ISurveyGroupData
    {
        public string Title { get; set; }
        public virtual List<SurveyData> Surveys { get; set; } = new();
    }
}
