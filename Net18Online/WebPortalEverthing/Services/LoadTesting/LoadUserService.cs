using Everything.Data.Repositories;

namespace WebPortalEverthing.Services.LoadTesting
{
    public class LoadUserService
    {
        private LoadAuthService _loadAuthService;
        private ILoadUserRepositryReal _loadUserRepositryReal;

        public const string DEFAULT_AVATAR = "~/images/LoadTesting/avatar.jpg";

        public LoadUserService(LoadAuthService loadAuthService, ILoadUserRepositryReal loadUserRepositryReal)
        {
            _loadAuthService = loadAuthService;
            _loadUserRepositryReal = loadUserRepositryReal;
        }

        public string GetAvatar()
        {
            var userId = _loadAuthService.GetUserId();
            if (userId is null)
            {
                return DEFAULT_AVATAR;
            }

            var user = _loadUserRepositryReal.Get(userId.Value);
            return user.AvatarUrl;
        }
    }
}
