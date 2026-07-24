using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        User Register(string username, string password); 
        User Login(string username, string password);
        bool Exists(string username);
    }
}