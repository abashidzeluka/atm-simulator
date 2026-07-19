using Domain.Enums;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public Role Role { get; set; }

        
        public Account(string userName, string password, decimal balance, Role role, int id)
        {
            Id = id; 
            UserName = userName;
            Balance = balance;
            Password = password;
            Role = role;
        }
    }
}
