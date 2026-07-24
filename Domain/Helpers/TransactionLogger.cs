using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Domain.Helpers
{
    public class TransactionEntry
    {
        public string Username { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class TransactionLogger
    {
        private string _path = @"C:\Users\User\source\repos\atm-simulator\Repository\Data\transactions.json";

        public void Log(string username, string type, decimal amount)
        {
            var transactions = LoadAll();

            transactions.Add(new TransactionEntry
            {
                Username = username,
                Type = type,
                Amount = amount,
                Date = DateTime.Now
            });

            string json = JsonConvert.SerializeObject(transactions, Formatting.Indented);
            File.WriteAllText(_path, json);
        }

        private List<TransactionEntry> LoadAll()
        {
            if (!File.Exists(_path))
                return new List<TransactionEntry>();

            string json = File.ReadAllText(_path);

            if (string.IsNullOrWhiteSpace(json))
                return new List<TransactionEntry>();

            return JsonConvert.DeserializeObject<List<TransactionEntry>>(json);
        }
    }
}
