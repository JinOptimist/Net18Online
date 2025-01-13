using Enums.Users;
using Everything.Data.Interface.Repositories;
using Everything.Data.Models;
using Everything.Data.Models.SqlRawModels;
using Everything.Data.Models.SqlRawModels.LoadTesting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Repositories
{
    public interface ILoadUserRepositryReal : ILoadUserRepositry<LoadUserData>
    {
        LoadUserData? Login(string login, string password);
        void Register(string login, string password, string email);
        void Register(string login, string password, string email, Role role = Role.User);
        void UpdateRole(int userId, Role role);
        public string GetAvatarUrl(int userId);
        void UpdateAvatarUrl(int userId, string avatarUrl);
        public bool IsAdminExist();
        void UpdateLocal(int userId, Language language);
        public IEnumerable<LoadUserByRoleData> GetUsersGroupedByRole();
    }

    public class LoadUserRepository : BaseRepository<LoadUserData>, ILoadUserRepositryReal
    {
        public LoadUserRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public override int Add(LoadUserData data)
        {
            throw new NotImplementedException("User method Register to create a new User");
        }

        public LoadUserData? Login(string login, string password)
        {
            var brokenPassword = BrokePassword(password);

            return _dbSet.FirstOrDefault(x => x.Login == login && x.Password == brokenPassword);
        }
        public string GetAvatarUrl(int userId)
        {
            return _dbSet.First(x => x.Id == userId).AvatarUrl;
        }

        public bool IsAdminExist()
        {
            return _dbSet.Any(x => x.Role.HasFlag(Role.Admin));
        }

        public void Register(string login, string password, string email)
        {

            var user = new LoadUserData
            {
                Login = login,
                Password = BrokePassword(password),
                Email = email,
                Coins = 100,
            };

            _dbSet.Add(user);
            _webDbContext.SaveChanges();
        }

        public void Register(string login, string password, string email, Role role = Role.User)
        {

            var user = new LoadUserData
            {
                Login = login,
                Password = BrokePassword(password),
                Email = email,
                Coins = 100,
                Role = role
            };

            _dbSet.Add(user);
            _webDbContext.SaveChanges();
        }

        private string BrokePassword(string originalPassword)
        {
            // jaaaack
            // jacke
            // jack
            var brokenPassword = originalPassword.Replace("pf", "");

            // jck
            return brokenPassword;
        }


        public void UpdateRole(int userId, Role role)
        {
            var user = _dbSet.First(x => x.Id == userId);
            user.Role = role;
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

        public IEnumerable<LoadUserByRoleData> GetUsersGroupedByRole()
        {
            var sql = @"SELECT 
                        Login
                        ,Role
                    FROM 
                        [dbo].[LoadUsers]
                    ORDER BY 
                        Role;";

            var result = _webDbContext
                .Database
                .SqlQueryRaw<LoadUserByRoleData>(sql)
                .ToList();

            return result;
        }

        public void Delete(int userId)
        {
            var user = _dbSet.First(x => x.Id == userId);
            _dbSet.Remove(user);
            _webDbContext.SaveChanges();
        }
    }
}
