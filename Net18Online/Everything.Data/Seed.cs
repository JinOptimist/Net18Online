using Enums.Users;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Everything.Data
{
    public class Seed
    {
        public void Fill(IServiceProvider service)
        {
            using var di = service.CreateScope();

            UserFill(di);
        }

        private void UserFill(IServiceScope di)
        {
            var userRepositry = di.ServiceProvider.GetRequiredService<IUserRepositryReal>();
            if (userRepositry.IsAdminExist())
            {
                return;
            }

            userRepositry.Register("admin", "admin", 30, Role.Admin);
        }
    }
}
