namespace Everything.Data.Interface.Models.Surveys
{
    public interface ISurvey
    {
        int Id { get; set; }
        int IdGroup { get; set; }
        int IdStatus { get; set; }
        string Title { get; set; }
    }
}
