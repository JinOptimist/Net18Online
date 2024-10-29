﻿using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class UserData : BaseModel, IUser
    {
        public string Login { get; set; }
        public decimal Coins { get; set; }
        public string AvatarUrl { get; set; }
    }
}
