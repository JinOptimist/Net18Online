﻿using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class MovieData : BaseModel, IMovieData
    {
        public string Name { get; set; }
        public string ImageSrc { get; set; }

        public virtual UserData? Creator { get; set; }
        public virtual FilmDirectorData? FilmDirector { get; set; }
        // public List<string> Tags { get; set; }
    }
}