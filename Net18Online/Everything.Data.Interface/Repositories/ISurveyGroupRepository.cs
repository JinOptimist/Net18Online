using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Interface.Repositories
{
    public interface ISurveyGroupRepository<T> : IBaseRepository<T>
        where T : ISurveyGroupData
    {
    }
}
