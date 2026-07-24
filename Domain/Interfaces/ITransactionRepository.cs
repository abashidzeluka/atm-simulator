using Domain.Entities;
using System.Collections.Generic;

namespace Repository.Interfaces
{
    public interface ITransactionRepository
    {
        void Add(Transaction transaction);
        List<Transaction> GetByUsername(string username);
    }
}