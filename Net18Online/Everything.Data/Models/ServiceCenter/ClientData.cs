﻿using Everything.Data.Interface.Models;

namespace Everything.Data.Models
{
    public class ClientData : BaseModel, IClientData
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; } = "-";
        public string Surname { get; set; } = "-";
        public string SecondName { get; set; } = "-";
        public string PassportNumber { get; set; } = "-";
        public string Password { get; set; }
        public int PostIndex { get; set; } = 0;
        public decimal Balance { get; set; }
        public string Role { get; set; }
    }
}
