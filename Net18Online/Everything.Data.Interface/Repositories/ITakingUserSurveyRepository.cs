using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Interface.Repositories
{
    public interface ITakingUserSurveyRepository<T> : IBaseRepository<T>
        where T : ITakingUserSurveyData
    {
        int Add(int userId, int surveyId);
    }
}