using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Interface.Repositories
{
    public interface ISurveysRepository<T> : IBaseRepository<T>
        where T : ISurveyData
    {
        void CreateSurvey(string title, int groupId, string? description);
    }
}
