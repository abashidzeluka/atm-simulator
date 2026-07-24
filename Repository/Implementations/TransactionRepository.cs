using Domain.Entities;
using Newtonsoft.Json;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string _filePath = "C:\\Users\\User\\source\\repos\\atm-simulator\\Repository\\Data\\Transactions.json";

        public TransactionRepository()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private List<Transaction> LoadAll()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<Transaction>();

                string json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                    return new List<Transaction>();

                return JsonConvert.DeserializeObject<List<Transaction>>(json) ?? new List<Transaction>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading transactions: {ex.Message}");
                return new List<Transaction>();
            }
        }

        private void SaveAll(List<Transaction> transactions)
        {
            try
            {
                string json = JsonConvert.SerializeObject(transactions, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving transactions: {ex.Message}");
            }
        }

        public void Add(Transaction transaction)
        {
            var transactions = LoadAll();
            transactions.Add(transaction);
            SaveAll(transactions);
        }

        public List<Transaction> GetByUsername(string username)
        {
            return LoadAll()
                .Where(t => t.Username != null && t.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}