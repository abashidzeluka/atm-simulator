using Application.Interfaces;
using Domain.Entities;
using Domain.Helpers;
using Repository.Implementations;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private IAccountRepository _repository;

        public AuthService(IAccountRepository repository)
        {
            _repository = repository;
        }
        public Account Register(string username, string password)
        {
            if (_repository.Exists(username))
            {
                Console.WriteLine("Username already taken!");
                return null;
            }

            Account account = new Account(username, Hasher.Hash(password), 0);
            string rawPin = account.Pin;
            account.Pin = Hasher.Hash(rawPin);
            _repository.Add(account);
            Console.WriteLine($"Your PIN is: {rawPin} — save it!");
            return account;
        }

        public Account Login(string username, string pin)
        {
            if (!_repository.Exists(username))
            {
                Console.WriteLine("Account not found!");
                return null;
            }

            Account account = _repository.GetByUsername(username);

            if (account.Pin != Hasher.Hash(pin))
            {
                Console.WriteLine("Wrong PIN!");
                return null;
            }

            Console.WriteLine("Login successful!");
            return account;
        }
    }
}
