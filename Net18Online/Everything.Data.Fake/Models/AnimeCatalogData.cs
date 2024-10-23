﻿using Everything.Data.Interface.Models;

namespace Everything.Data.Fake.Models
{
    public class AnimeCatalogData : BaseModel, IAnimeCatalogData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }
    }
}