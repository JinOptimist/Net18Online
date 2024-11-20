﻿using Everything.Data.Interface.Models;
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
        
        public IEnumerable<EcologyData>? Ecologies { get; set; }
        public IEnumerable<CommentData>? Comments { get; set; }
        
        public virtual List<SurveyGroupData> СreatorSurveyGroups { get; set; } = new();
        public virtual List<GirlData> CreatedGirls { get; set; } = new();
        public virtual List<MangaData> CreatedMangas { get; set; } = new();
        public virtual List<CoffeData> CreatedCoffe { get; set; } = new();
        public virtual List<AnimeReviewData> CreatedAnimeReviews { get; set; } = new();
    }
}
