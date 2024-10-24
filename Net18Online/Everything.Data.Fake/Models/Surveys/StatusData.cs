using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Fake.Models.Surveys
{
    public class StatusData : IStatus
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagesSrc { get; set; }
    }
}
