using Application.Services;
using Domain.Entities;
using Repository.Implementations;
using UI.Menus;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repository = new AccountRepository();
            var authService = new AuthService(repository);
            var authMenu = new ClientMenu(authService);

            authMenu.Show();


        }

    }
}
