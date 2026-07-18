using System;
using Application.Interfaces;
using Spectre.Console;



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
            AnsiConsole.Write(new FigletText("ATM").Color(Color.Green));

            while (true)
            {
                AnsiConsole.WriteLine();
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()                  
                        .Title("Welcome to ATM")
                        .AddChoices("Register", "Login", "Exit"));

                switch (choice)
                {
                    case "Register":
                        Register();
                        break;
                    case "Login":
                        Login();
                        break;
                    case "Exit":
                        AnsiConsole.MarkupLine("[green]Goodbye![/]");
                        return;
                }
            }
        }

        private void Register()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                AnsiConsole.MarkupLine("[red]Username cannot be empty![/]");
                return;
            }
            if (username.Length < 3)
            {
                AnsiConsole.MarkupLine("[red]Username must be at least 3 characters![/]");
                return;
            }
            if (!username.All(char.IsLetter))
            {
                AnsiConsole.MarkupLine("[red]Username must contain only letters![/]");
                return;
            }

            Console.Write("Create a password: ");
            string password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                AnsiConsole.MarkupLine("[red]Password cannot be empty![/]");
                return;
            }
            if (password.Length < 6)
            {
                AnsiConsole.MarkupLine("[red]Password must be at least 6 characters![/]");
                return;
            }
            if (!password.Any(char.IsDigit))
            {
                AnsiConsole.MarkupLine("[red]Password must contain at least one number![/]");
                return;
            }
            if (!password.Any(char.IsUpper))
            {
                AnsiConsole.MarkupLine("[red]Password must have at least one uppercase letter![/]");
                return;
            }

            var account = _authService.Register(username, password);
            if (account != null)
            {
                AnsiConsole.MarkupLine("[green]Account created![/]");
            }
            else
                AnsiConsole.MarkupLine("[red]Username already taken![/]");

        }

        private void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine().Trim();

            int attempts = 0;
            while (attempts < 3)
            {
                Console.Write("Enter a password: ");
                string password = Console.ReadLine();

             var account = _authService.Login(username, password);

            if (account != null)
            {
                AnsiConsole.MarkupLine("[green]Welcome " + account.UserName + "![/]");
                return;
            }
            else
            {
                attempts++;
                AnsiConsole.MarkupLine($"[red]Login failed! {3 - attempts} attempts left.[/]");
            }
            }
                AnsiConsole.MarkupLine("[red]Account locked! Too many failed attempts.[/]");
        }
    }
}
