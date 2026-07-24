using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Helpers;
using Repository.Implementations;
using UI.Menus;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var repository = new UserRepository();
            var loanRepository = new LoanRepository();
            var transactionLogger = new TransactionLogger();
            var authService = new AuthService(repository);
            var loanService = new LoanService(loanRepository, repository);
            var transactionService = new TransactionService(repository, transactionLogger);
            var authMenu = new ClientMenu(authService, transactionService, loanService);

            authMenu.Show();


        }

    }
}
