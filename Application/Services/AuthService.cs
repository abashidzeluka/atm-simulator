using Application.Interfaces;
using Domain.Entities;
using Domain.Helpers;
using Repository.Interfaces;
using System;

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
                return null;

            Account account = new Account(username, Hasher.Hash(password), 0);

            _repository.Add(account);
            return account;
        }

        public Account Login(string username, string password)
        {
            if (!_repository.Exists(username))
            {
                return null;
            }
            Account account = _repository.GetByUsername(username);

            if (account.Password != Hasher.Hash(password))
            {
                return null;
            }
            return account;
        }
    }
}
