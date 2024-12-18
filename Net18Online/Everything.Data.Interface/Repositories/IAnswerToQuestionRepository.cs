using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Interface.Repositories
{
    public interface IAnswerToQuestionRepository<T> : IBaseRepository<T>
        where T : IAnswerToQuestionData
    {
        void SetTextValue(int id, string? value);
    }
}
