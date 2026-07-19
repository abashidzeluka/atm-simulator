using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Account Register(string username, string password);
        Account Login(string username, string password);
        bool Exists(string username);
    }
}