namespace Everything.Data.Interface.Models.Surveys
{
    public interface ISurveyData : IBaseModel
    {
        int IdStatus { get; set; }
        string Title { get; set; }
    }
}
