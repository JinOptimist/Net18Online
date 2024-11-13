using Everything.Data.Interface.Models;
using Everything.Data.Models.Surveys;

namespace Everything.Data.Models
{
    public class UserData : BaseModel, IUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public decimal Coins { get; set; }
        public string AvatarUrl { get; set; }
        public virtual List<SurveyGroupData> СreatorSurveyGroups { get; set; } = new();
    }
}
