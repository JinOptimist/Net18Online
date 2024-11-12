﻿using Everything.Data.Interface.Repositories;
using Everything.Data.Migrations;
using Everything.Data.Models;

namespace Everything.Data.Repositories
{
    public interface IUserRepositryReal : IUserRepositry<UserData>
    {
        UserData? Login(string login, string password);
        void Register(string login, string password, int age);
    }

    public class UserRepository : BaseRepository<UserData>, IUserRepositryReal
    {
        public UserRepository(WebDbContext webDbContext) : base(webDbContext)
        {
        }

        public override void Add(UserData data)
        {
            throw new NotImplementedException("User method Register to create a new User");
        }

        public UserData? Login(string login, string password)
        {
            var brokenPassword = BrokePassword(password);

            return _dbSet.FirstOrDefault(x => x.Login == login && x.Password == brokenPassword);
        }

        public void Register(string login, string password, int age)
        {

            var user = new UserData
            {
                Login = login,
                Password = BrokePassword(password),
                Age = age,
                Coins = 100,
                AvatarUrl = "/images/avatar/default.png"
            };

            _dbSet.Add(user);
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