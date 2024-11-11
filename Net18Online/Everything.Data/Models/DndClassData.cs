﻿using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class DndClassData : BaseModel, IDNDData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public virtual List<DndSubClassData> SubClasses { get; set; } = new List<DndSubClassData>();
    }
}
