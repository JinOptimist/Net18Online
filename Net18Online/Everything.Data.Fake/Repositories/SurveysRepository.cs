using Everything.Data.Interface.Models.Surveys;
using Everything.Data.Interface.Repositories;

namespace Everything.Data.Fake.Repositories
{
    public class SurveysRepository : ISurveysRepository
    {
        private List<ISurveyGroup> surveyGroups = new();
        private List<ISurvey> surveys = new();
        private List<IStatus> statuses = new();

        // surveys
        public void AddSurvey(ISurvey data)
        {
            data.Id = surveys.Any()
                ? surveys.Max(x => x.Id) + 1
                : 1;

            surveys.Add(data);
        }

        public void Delete(ISurvey data)
        {
            surveys.Remove(data);
        }

        public List<ISurvey> GetAllSurveys()
        {
            return surveys;
        }

        public ISurvey? Get(int id)
        {
            return surveys.FirstOrDefault(x => x.Id == id);
        }

        public bool AnySurveys()
        {
            return surveys.Any();
        }

        // surveyGroups
        public void AddSurveyGroup(ISurveyGroup data)
        {
            data.Id = surveyGroups.Any()
                ? surveyGroups.Max(x => x.Id) + 1
                : 1;

            surveyGroups.Add(data);
        }

        public List<ISurveyGroup> GetAllSurveyGroups()
        {
            return surveyGroups;
        }

        public bool AnySurveyGroups()
        {
            return surveyGroups.Any();
        }

        // statuses
        public void AddStatus(IStatus data)
        {
            statuses.Add(data);
        }

        public List<IStatus> GetAllStatuses()
        {
            return statuses;
        }

        public bool AnyStatuses()
        {
            return statuses.Any();
        }
    }
}
