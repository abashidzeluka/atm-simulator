using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Account
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }

        public Account(string userName, string password, decimal balance)
        {
            Id = Guid.NewGuid().ToString();
            UserName = userName;
            Balance = balance;
            Password = password;
        }
    }
}
