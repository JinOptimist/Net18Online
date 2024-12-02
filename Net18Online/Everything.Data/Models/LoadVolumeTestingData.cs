using Everything.Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Models
{
    public class LoadVolumeTestingData : BaseModel, ILoadVolumeTestingData
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public UserData? Author { get; set; }

        public virtual LoadUserData LoadUserDataCreator { get; set; }
        public virtual List<MetricData> VolumeMetrics { get; set; } = new List<MetricData>();
    }
}
