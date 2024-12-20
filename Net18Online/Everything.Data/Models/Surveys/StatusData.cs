﻿using Everything.Data.Interface.Models.Surveys;

namespace Everything.Data.Models.Surveys
{
    public class StatusData : BaseModel, IStatusData
    {
        public string Title { get; set; }
        public string ImagesSrc { get; set; }
    }
}
