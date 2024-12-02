using Enums.Users;
using Everything.Data.Models;
using Everything.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Everything.Data
{
    public class LoadTestingSeed
    {
        public void Fill(IServiceProvider service)
        {
            using var di = service.CreateScope();

            LoadUserFill(di);
        }

        private void LoadUserFill(IServiceScope di)
        {
            var LoadUserRepositry = di.ServiceProvider.GetRequiredService<ILoadUserRepositryReal>();
            if (LoadUserRepositry.IsAdminExist())
            {
                return;
            }

            LoadUserRepositry.Register("admin", "admin", "admin@test.ru", Role.Admin);
        }
    }
}
