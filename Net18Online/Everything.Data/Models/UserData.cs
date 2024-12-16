using Enums.Users;
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
        public int NumberOfImagesCreated { get; set; }

        public Language Language { get; set; }
        public Role Role { get; set; }

        public IEnumerable<EcologyData>? Ecologies { get; set; }
        public ICollection<UserEcologyLikesData> PostsWhichUsersLike { get; set; } = new List<UserEcologyLikesData>();
        public IEnumerable<CommentData>? Comments { get; set; }

        public virtual List<SurveyGroupData> СreatorSurveyGroups { get; set; } = new();
        public virtual List<GirlData> CreatedGirls { get; set; } = new();
        public virtual List<MangaData> CreatedMangas { get; set; } = new();

        public virtual List<GameData> Games { get; set; } = new List<GameData>();
        public virtual List<CoffeData> CreatedCoffe { get; set; } = new();
        public virtual List<AnimeReviewData> CreatedAnimeReviews { get; set; } = new();
        public virtual List<MovieData> CreatedMovies { get; set; } = new();
        public virtual List<FilmDirectorData> CreatedFilmDirectors { get; set; } = new();
        public virtual List<CakeData> CreatedCakes { get; set; } = new();
        public virtual List<MagazinData> CreatedMagazins { get; set; } = new();
        public virtual List<TakingUserSurveyData> PassingSurveys { get; set; } = new();
        public virtual List<GirlData> GirlsWhichUsersLike { get; set; }

        public virtual List<GameData> GameWhichUsersLike { get; set; }
        public virtual List<GameData> GameWhichUsersDislike { get; set; }
        public virtual List<ChatMessageData> ChatMessages { get; set;} = new();
        public virtual List<CoffeChatMessageData> CoffeChatMessages { get; set;} = new();
    }
}
