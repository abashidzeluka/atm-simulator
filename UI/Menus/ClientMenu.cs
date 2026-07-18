using Application.Interfaces;
using System;


namespace UI.Menus
{
    public class ClientMenu
    {
        private IAuthService _authService;

        public ClientMenu(IAuthService authService)
        {
            _authService = authService;
        }

        public void Show()
        {
            Console.WriteLine("--- Welcome to ATM ---");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.Write("Choose: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Register();
                    break;
                case "2":
                    Login();
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }

            private void Register()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine().Trim();

            Console.Write("Create a password: ");
            string password = Console.ReadLine();

            var account = _authService.Register(username, password);
        }

        private void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine().Trim();

            Console.Write("Enter PIN: ");
            string pin = Console.ReadLine().Trim();

            var account = _authService.Login(username, pin);

            if (account != null)
                Console.WriteLine("Welcome " + account.UserName + "!");
        }
    }
}
