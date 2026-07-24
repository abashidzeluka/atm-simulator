using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;

namespace Domain.Entities
{
    public class Client : User
    {
        public decimal Balance { get; set; }

        public Client(int id, string userName, string password, decimal balance)
            : base(id, userName, password, Role.Client)
        {
            Balance = balance;
        }
    }
}
