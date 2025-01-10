
using Enums.Users;
using System.Globalization;
using WebPortalEverthing.Localizations;

namespace WebPortalEverthing.Services.LoadTesting
{
    //   public class LoadAuthService : AuthService
    public class LoadAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public const string AUTH_TYPE_KEY = "ByPass";
        public const string CLAIM_TYPE_ID = "Id";
        public const string CLAIM_TYPE_NAME = "Name";
        public const string CLAIM_TYPE_COINS = "Coins";
        public const string CLAIM_TYPE_ROLE = "Role";

        /*     public LoadAuthService(IHttpContextAccessor httpContextAccessor)
                  : base(httpContextAccessor) // Вызов конструктора родительского класса
             {
                 _httpContextAccessor = httpContextAccessor;
             }*/
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
            return GetClaimValue(CLAIM_TYPE_NAME) ?? LoadStuffs.Guest;
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

            // Попробуем привести строку в корректный формат и распарсить её
            if (decimal.TryParse(
                isStr.Replace(',', '.'), // Заменяем запятую на точку, если нужно
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out var value))
            {
                return value;
            }

            // Если парсинг не удался, возвращаем null или бросаем исключение
            return null;
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
