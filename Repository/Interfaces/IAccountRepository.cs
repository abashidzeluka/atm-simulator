using System;
using Domain.Entities;

namespace Repository.Interfaces
{
    public interface IAccountRepository
    {    
            Account GetByUsername(string username);  
            void Add(Account account);               
            bool Exists(string username);            
            void Update(Account account);
    }
}
