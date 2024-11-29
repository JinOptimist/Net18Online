using Enums.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everything.Data.Interface.Models
{
    public interface ILoadUserData : IBaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal Coins { get; set; }
        public DateTime CreatedAt { get; set; }
        public Role Role { get; set; }
        public string AvatarUrl { get; set; }
        public Language Language { get; set; }
    }
}
