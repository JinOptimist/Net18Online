using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Interface.Repositories
{
    public interface ISurveysRepository
    {
        void AddSurvey(ISurvey data);

        void Delete(ISurvey data);

        List<ISurvey> GetAllSurveys();

        ISurvey? Get(int id);

        bool AnySurveys();

        void AddSurveyGroup(ISurveyGroup data);

        List<ISurveyGroup> GetAllSurveyGroups();

        bool AnySurveyGroups();

        void AddStatus(IStatus data);

        List<IStatus> GetAllStatuses();

        bool AnyStatuses();
    }
}
