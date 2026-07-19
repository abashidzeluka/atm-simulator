using Application.Interfaces;
using Domain.Entities;
using Spectre.Console;
using System;
using System.Security.Principal;



namespace UI.Menus
{
    public class ClientMenu
    {
        private IAuthService _authService;
        private Account _account;
        private ITransactionService _transactionService;

        public ClientMenu(IAuthService authService, ITransactionService transactionService)
        {
            _authService = authService;
            _transactionService = transactionService;
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
            string username;

                while (true)
                {
                    Console.Write("Enter username: ");
                    username = Console.ReadLine().Trim();

                    if (string.IsNullOrWhiteSpace(username))
                        AnsiConsole.MarkupLine("[red]Username cannot be empty![/]");
                    else if (username.Length < 3)
                        AnsiConsole.MarkupLine("[red]Username must be at least 3 characters![/]");
                    else if (!username.All(char.IsLetter))
                        AnsiConsole.MarkupLine("[red]Username must contain only letters![/]");
                    else
                        break;
                }

            string password;
                while(true)
                {
                    Console.Write("Create a password: ");
                    password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(password))
                    AnsiConsole.MarkupLine("[red]Password cannot be empty![/]");
                else if (password.Length < 6)
                    AnsiConsole.MarkupLine("[red]Password must be at least 6 characters![/]");
                else if (!password.Any(char.IsDigit))
                    AnsiConsole.MarkupLine("[red]Password must contain at least one number![/]");
                else if (!password.Any(char.IsUpper))
                    AnsiConsole.MarkupLine("[red]Password must have at least one uppercase letter![/]");
                else
                    break;
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
            string username;
            while (true)
            {
                Console.Write("Enter username: ");
                username = Console.ReadLine().Trim();

                if (string.IsNullOrWhiteSpace(username))
                    AnsiConsole.MarkupLine("[red]Username cannot be empty![/]");
                else if (username.Length < 3)
                    AnsiConsole.MarkupLine("[red]Username must be at least 3 characters![/]");
                else if (!username.All(char.IsLetter))
                    AnsiConsole.MarkupLine("[red]Username must contain only letters![/]");
                else if (!_authService.Exists(username))
                    AnsiConsole.MarkupLine("[red]Username not found! Try again.[/]");
                else
                    break;
            }

            int attempts = 0;
            while (attempts < 3)
            {
                Console.Write("Enter a password: ");
                string password = Console.ReadLine();

                var account = _authService.Login(username, password);

                if (account != null)
                {
                    _account = account;
                    AnsiConsole.MarkupLine("[green]Welcome " + account.UserName + "![/]");
                    ShowAtmMenu();
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

        private void ShowAtmMenu()
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"Hello [green]{_account.UserName}[/]!")
                        .AddChoices("Check Balance", "Deposit", "Withdraw", "Logout"));

                switch (choice)
                {
                    case "Check Balance":
                        CheckBalance();
                        break;
                    case "Deposit":
                        Deposit();
                        break;
                    case "Withdraw":
                        Withdraw();
                        break;
                    case "Logout":
                        _account = null;
                        return;
                }
            }
        }

        private void CheckBalance()
        {
            decimal balance = _transactionService.GetBalance(_account.UserName);
            AnsiConsole.MarkupLine($"[green]Your balance is: {balance} GEL[/]");
        }

        private void Deposit()
        {
            Console.Write("Enter amount to deposit: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            _transactionService.Deposit(_account.UserName, amount);
            AnsiConsole.MarkupLine($"[green]Successfully deposited {amount} GEL![/]");
        }

        private void Withdraw()
        {           
                

            while(true)
            {
                Console.Write("Enter amount to withdraw: ");
                decimal amount = decimal.Parse(Console.ReadLine());
                
                if (_account.Balance < amount)
                    AnsiConsole.MarkupLine($"[red]Not enough amount! try again[/]");

                else
                {
                    _transactionService.Withdraw(_account.UserName, amount);
                    AnsiConsole.MarkupLine($"[green]Successfully withdrawn {amount} GEL![/]");
                    break;
                }
            }
            
        }
    }
}
