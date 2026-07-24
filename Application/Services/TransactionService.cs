using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Repository.Interfaces;
using System;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUserRepository _repository;
        private readonly TransactionLogger _transactionLogger;

        public TransactionService(IUserRepository repository, TransactionLogger transactionLogger)
        {
            _repository = repository;
            _transactionLogger = transactionLogger;
        }

        private Client GetClient(string username)
        {
            if (_repository.GetByUsername(username) is not Client client)
                throw new Exception("User is not a client!");

            return client;
        }

        public decimal GetBalance(string username)
        {
            return GetClient(username).Balance;
        }

        public void Withdraw(string username, decimal amount)
        {   
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            Client client = GetClient(username);

            if (client.Balance < amount)
                throw new InvalidOperationException("Insufficient funds.");

            client.Balance -= amount;
            _transactionLogger.Log(username, "Withdraw", amount);
            _repository.Update(client);
        }

        public void Deposit(string username, decimal amount)
        {

            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            Client client = GetClient(username);

            client.Balance += amount;
            _transactionLogger.Log(username, "Deposit", amount);
            _repository.Update(client);
        }


    }
}
