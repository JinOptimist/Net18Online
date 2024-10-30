namespace Everything.Data.Interface.Models.Surveys
{
    public interface ISurveyData : IBaseModel
    {
        int IdGroup { get; set; }
        int IdStatus { get; set; }
        string Title { get; set; }
    }
}
