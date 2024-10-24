using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Fake.Models.Surveys
{
    public class SurveyData : ISurvey
    {
        public int Id { get; set; }
        public int IdGroup { get; set; }
        public int IdStatus { get; set; }
        public string Title { get; set; }
    }
}
