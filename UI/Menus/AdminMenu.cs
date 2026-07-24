using Application.Interfaces;
using Spectre.Console;
using System;

namespace UI.Menus
{
    public class AdminMenu
    {
        private readonly ILoanService _loanService;

        public AdminMenu(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine();
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[red]Admin Menu[/]")
                        .AddChoices(
                            "View Pending Loans",
                            "Approve Loan",
                            "Decline Loan",
                            "Logout"));

                switch (choice)
                {
                    case "View Pending Loans":
                        ViewPendingLoans();
                        break;

                    case "Approve Loan":
                        ApproveLoan();
                        break;

                    case "Decline Loan":
                        DeclineLoan();
                        break;

                    case "Logout":
                        return;
                }
            }
        }
        private void ViewPendingLoans()
        {
            var loans = _loanService.GetPendingLoans();

            if (loans == null || loans.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]There are no pending loans.[/]");
                return;
            }

            var table = new Table();

            table.AddColumn("Loan ID");
            table.AddColumn("User ID");
            table.AddColumn("Amount");
            table.AddColumn("Request Date");

            foreach (var loan in loans)
            {
                table.AddRow(
                    loan.LoanId.ToString(),
                    loan.UserId.ToString(),
                    $"{loan.Amount} GEL",
                    loan.RequestDate.ToString("dd.MM.yyyy HH:mm")
                );
            }

            AnsiConsole.Write(table);
        }

        private void ApproveLoan()
        {
            Console.Write("Enter Loan ID to approve: ");
            if (!int.TryParse(Console.ReadLine(), out int loanId))
            {
                AnsiConsole.MarkupLine("[red]Invalid Loan ID![/]");
                return;
            }

            try
            {
                _loanService.LoanApprove(loanId);
                AnsiConsole.MarkupLine("[green]Loan approved successfully![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }

        private void DeclineLoan()
        {
            Console.Write("Enter Loan ID to decline: ");
            if (!int.TryParse(Console.ReadLine(), out int loanId))
            {
                AnsiConsole.MarkupLine("[red]Invalid Loan ID![/]");
                return;
            }

            try
            {
                _loanService.LoanDecline(loanId);
                AnsiConsole.MarkupLine("[yellow]Loan declined successfully![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }
    
    }
        
}
