using Application.Interfaces;
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
            var loanRepository = new LoanRepository();
            var authService = new AuthService(repository);
            var loanService = new LoanService(loanRepository);
            var transactionService = new TransactionService(repository);
            var authMenu = new ClientMenu(authService, transactionService, loanService);

            authMenu.Show();


        }

    }
}
