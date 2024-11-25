using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Interface.Repositories
{
    public interface ISurveysRepository<T> : IBaseRepository<T>
        where T : ISurveyData
    {
        T GetWithGroupAndQuestions(int id);
        void CreateSurvey(string title, int groupId, string? description);
        void UpdateTitle(int id, string newTitle);
        void UpdateDescription(int id, string? description);
    }
}
