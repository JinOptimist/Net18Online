using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Models.Surveys
{
    public class SurveyData : BaseModel, ISurveyData
    {
        public virtual SurveyGroupData SurveyGroup { get; set; }
        public int IdStatus { get; set; }
        public string Title { get; set; }
    }
}
