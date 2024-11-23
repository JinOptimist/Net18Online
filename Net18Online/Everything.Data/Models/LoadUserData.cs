using Enums.Users;
using Everything.Data.Interface.Models;
using Everything.Data.Models.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Models
{
    public class LoadUserData : BaseModel, ILoadUserData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public decimal Coins { get; set; }
        public Role Role { get; set; } = Role.Observer;

        public virtual List<MetricData> Metrics { get; set; } = new();
        public virtual List<LoadVolumeTestingData> LoadVolumeTestingParts { get; set; } = new();
    }
}
