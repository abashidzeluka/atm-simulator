using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ITransactionService
    {
        decimal GetBalance(string username);
        void Deposit(string username, decimal amount);
        void Withdraw(string username, decimal amount);
    }
}
