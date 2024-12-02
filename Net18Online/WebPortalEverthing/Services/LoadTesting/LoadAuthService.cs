
using Enums.Users;

namespace WebPortalEverthing.Services.LoadTesting
{
    public class LoadAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public const string AUTH_TYPE_KEY = "ByPass";
        public const string CLAIM_TYPE_ID = "Id";
        public const string CLAIM_TYPE_NAME = "Name";
        public const string CLAIM_TYPE_COINS = "Coins";
        public const string CLAIM_TYPE_ROLE = "Role";

        public LoadAuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsAuthenticated()
        {
            return GetUserId() is not null;
        }

        public string? GetName()
        {
            return GetClaimValue(CLAIM_TYPE_NAME) ?? "Гость";
        }

        public int? GetUserId()
        {
            var isStr = GetClaimValue(CLAIM_TYPE_ID);
            if (isStr == null)
            {
                return null;
            }

            return int.Parse(isStr);
        }

        public Role GetRole()
        {
            var roleStr = GetClaimValue(CLAIM_TYPE_ROLE);
            if (roleStr is null)
            {
                return Role.Observer;
                // throw new Exception("Guest cant has a role");
            }
            var roleInt = int.Parse(roleStr);
            var role = (Role)roleInt;
            return role;
        }

        public String GetRoleStr()
        {
            return GetRole().ToString();
        }

        public bool IsAdmin()
        {
            return IsAuthenticated()
                   && (GetRole().HasFlag(Role.Admin)
                   || (GetName()?.Contains("admin") ?? false)
                   || (GetName()?.Contains("Admin") ?? false));
        }


        public bool HasRole(Role role)
        {
            return IsAuthenticated() && GetRole().HasFlag(role);
        }

        public decimal? GetUserCoins()
        {
            var isStr = GetClaimValue(CLAIM_TYPE_COINS);
            if (isStr == null)
            {
                return null;
            }

            return decimal.Parse(isStr);
        }



        private string? GetClaimValue(string type)
        {
            return _httpContextAccessor
                .HttpContext!
               .User
               .Claims
               .FirstOrDefault(x => x.Type == type)
               ?.Value;
        }

    }
}
