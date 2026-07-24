using Application.Interfaces;
using Domain.Entities;
using Domain.Helpers;
using Repository.Interfaces;
using Domain.Enums;
using System;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private IUserRepository _repository;
        private Logger _logger = new Logger();

        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }
        public User Register(string username, string password)
        {
            if (_repository.Exists(username))
                return null;

            int newId = _repository.Count() + 1;
            Client client = new Client(newId, username, Hasher.Hash(password), 0);

            _repository.Add(client);
            _logger.Log("Register", username);
            return client;
        }

        public User Login(string username, string password)
        {
            if (!_repository.Exists(username))
            {
                return null;
            }
            User account = _repository.GetByUsername(username);

            if (account.Password != Hasher.Hash(password))
            {
                return null;
            }
            _logger.Log("Login", username);
            return account;
        }

        public bool Exists(string username)
        {
            return _repository.Exists(username);
        }
    }
}
