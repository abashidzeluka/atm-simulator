using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Spectre.Console;
using System;
using System.ComponentModel;
using System.Linq;

namespace UI.Menus
{
    public class ClientMenu
    {
        private IAuthService _authService;
        private Account _account;
        private ITransactionService _transactionService;
        private ILoanService _loanService;

        public ClientMenu(IAuthService authService, ITransactionService transactionService, ILoanService loanService)
        {
            _authService = authService;
            _transactionService = transactionService;
            _loanService = loanService;
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
                Console.Write("Enter username (or type '0' to go back): ");
                username = Console.ReadLine().Trim();

                if (username == "0")
                {
                    return;
                }

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
            while (true)
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
                Console.WriteLine();
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

                Console.Write("Enter username (or type '0' to go back): ");
                username = Console.ReadLine().Trim();

                if (username == "0")
                {
                    return;
                }

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
                    ShowClientMenu();
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

        private void ShowClientMenu()
        {
            while (true)
            {
                Console.WriteLine();
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($" [green]Welcome, {_account.UserName}[/]!")
                        .AddChoices("Check Balance", "Deposit", "Withdraw", "Loan Options", "Logout", "Back"));

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
                    case "Loan Options":
                        LoanMenu();
                        break;
                    case "Back":
                        return;
                    case "Logout":
                        _account = null;
                        return;
                }
            }
        }

        private void CheckBalance()
        {
            decimal balance = _transactionService.GetBalance(_account.UserName);
            Console.WriteLine();
            AnsiConsole.MarkupLine($"[green]Your balance is: {balance} GEL[/]");
        }

        private void Deposit()
        {
            Console.WriteLine();
            Console.Write("Enter amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
            {
                _transactionService.Deposit(_account.UserName, amount);
                Console.WriteLine();
                AnsiConsole.MarkupLine($"[green]Successfully deposited {amount} GEL![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid amount![/]");
            }
        }

        private void Withdraw()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter amount to withdraw: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
                {
                    AnsiConsole.MarkupLine("[red]Please enter a valid positive number![/]");
                    continue;
                }

                decimal currentBalance = _transactionService.GetBalance(_account.UserName);

                if (currentBalance < amount)
                {
                    Console.WriteLine();
                    AnsiConsole.MarkupLine($"[red]Not enough funds! Your balance is {currentBalance} GEL[/]");
                }
                else
                {
                    _transactionService.Withdraw(_account.UserName, amount);
                    Console.WriteLine();
                    AnsiConsole.MarkupLine($"[green]Successfully withdrawn {amount} GEL![/]");
                    break;
                }
            }
        }

        private void LoanMenu()
        {
            while (true)
            {
                Console.WriteLine();
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]Loan Operations[/]")
                        .AddChoices("Request Loan", "My Loans", "Back"));

                switch (choice)
                {
                    case "Request Loan":
                        RequestLoan();
                        break;
                    case "My Loans":
                        ShowMyLoans();
                        break;
                    case "Back":
                        return;
                }
            }
        }

        private void RequestLoan()
        {
            Console.WriteLine();
            Console.Write("Enter desired loan amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                AnsiConsole.MarkupLine("[red]Invalid loan amount![/]");
                return;
            }

            try
            {
                // Calls LoanRequest from your LoanService
                _loanService.LoanRequest(_account.Id, amount);
                AnsiConsole.MarkupLine($"[green]Loan request for {amount} GEL submitted successfully! Status: Pending Approval.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error requesting loan: {ex.Message}[/]");
            }
        }

        private void ShowMyLoans()
        {
            Console.WriteLine();
            var loans = _loanService.GetUserLoans(_account.Id);

            if (loans == null || !loans.Any())
            {
                AnsiConsole.MarkupLine("[yellow]You have no active or previous loans.[/]");
                return;
            }

            var table = new Table();
            table.AddColumn("Loan ID");
            table.AddColumn("Amount (GEL)");
            table.AddColumn("Status");

            foreach (var loan in loans)
            {
                string statusColor = loan.Status switch
                {
                    LoanStatus.Approved => "green",
                    LoanStatus.Declined => "red",
                    _ => "yellow"
                };

                table.AddRow(
                    loan.LoanId.ToString(),
                    loan.Amount.ToString("0.00"),
                    $"[{statusColor}]{loan.Status}[/]"
                );
            }

            AnsiConsole.Write(table);
        }
    }
}