using Application.Interfaces;
using Domain.Entities;
using Repository.Interfaces;
using System;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private IAccountRepository _repository;

        public TransactionService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public decimal GetBalance(string username)
        {
            return _repository.GetByUsername(username).Balance;   
        }

        public void Withdraw(string username, decimal amount)
        {
            Account account = _repository.GetByUsername(username);
            account.Balance -= amount;
            _repository.Update(account);
        }

        public void Deposit(string username, decimal amount)
        {
            Account account = _repository.GetByUsername(username);
            account.Balance += amount;
            _repository.Update(account);
        }


    }
}
