using Enums.Users;
using Everything.Data.Interface.Repositories;
using Everything.Data.Migrations;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IUserRepositryReal : IUserRepositry<UserData>
    {
        string GetAvatarUrl(int userId);
        bool IsAdminExist();
        UserData? Login(string login, string password);
        void Register(string login, string password, int age, Role role = Role.User);
        void UpdateAvatarUrl(int userId, string avatarUrl);
        void UpdateLocal(int userId, Language language);
        void UpdateRole(int userId, Role role);
        int GetNewIdForImage(int userId);
    }

    public class UserRepository : BaseRepository<UserData>, IUserRepositryReal
    {
        public const int DEFAULT_VALUE_FOR_NUMBER_OF_IMAGES_CREATED = 0;
        public UserRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public override void Add(UserData data)
        {
            throw new NotImplementedException("User method Register to create a new User");
        }

        public string GetAvatarUrl(int userId)
        {
            return _dbSet.First(x => x.Id == userId).AvatarUrl;
        }

        public int GetNewIdForImage(int userId)
        {
            var user = _dbSet.FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return DEFAULT_VALUE_FOR_NUMBER_OF_IMAGES_CREATED;
            }

            var newIdForImage = ++user.NumberOfImagesCreated;
            _webDbContext.SaveChanges();

            return newIdForImage;
        }

        public bool IsAdminExist()
        {
            return _dbSet.Any(x => x.Role.HasFlag(Role.Admin));
        }

        public UserData? Login(string login, string password)
        {
            var brokenPassword = BrokePassword(password);

            return _dbSet.FirstOrDefault(x => x.Login == login && x.Password == brokenPassword);
        }

        public void Register(string login, string password, int age, Role role = Role.User)
        {
            var user = new UserData
            {
                Login = login,
                Password = BrokePassword(password),
                Age = age,
                Coins = 100,
                AvatarUrl = "/images/AnimeGirl/avatar-default.webp",
                Role = role,
                Language = Language.Ru,
                NumberOfImagesCreated = DEFAULT_VALUE_FOR_NUMBER_OF_IMAGES_CREATED,
            };

            _dbSet.Add(user);
            _webDbContext.SaveChanges();
        }

        public void UpdateAvatarUrl(int userId, string avatarUrl)
        {
            var user = _dbSet.First(x => x.Id == userId);
            user.AvatarUrl = avatarUrl;
            _webDbContext.SaveChanges();
        }

        public void UpdateLocal(int userId, Language language)
        {
            var user = _dbSet.First(x => x.Id == userId);

            user.Language = language;

            _webDbContext.SaveChanges();
        }

        public void UpdateRole(int userId, Role role)
        {
            var user = _dbSet.First(x => x.Id == userId);
            user.Role = role;
            _webDbContext.SaveChanges();
        }

        private string BrokePassword(string originalPassword)
        {
            // jaaaack
            // jacke
            // jack
            var brokenPassword = originalPassword.Replace("a", "");

            // jck
            return brokenPassword;
        }
    }
}
