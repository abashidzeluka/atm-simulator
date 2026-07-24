using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;

namespace Domain.Entities
{
    public class Admin : User
    {
        public Admin(int id, string userName, string password)
        : base(id, userName, password, Role.Admin)
        {
        }
    }
}
