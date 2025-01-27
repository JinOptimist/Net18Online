using Everything.Data.Interface.Enums;
using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Interface.Repositories
{
    public interface IQuestionRepository<T> : IBaseRepository<T>
        where T : IQuestionData
    {
        int Create(int surveyId);
        void UpdateTitle(int id, string value);
        void UpdateRequired(int id, bool value);
        void UpdateAnswerType(int id, AnswerType value);
        IEnumerable<IQuestionData> GetQuestionsForSurvey(int surveyId);
    }
}