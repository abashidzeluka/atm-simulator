using System;
using Domain.Entities;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {    
            User GetByUsername(string username);
            User GetById(int id);
            void Add(User account);               
            bool Exists(string username);            
            void Update(User account);
            int Count();
    }
}
