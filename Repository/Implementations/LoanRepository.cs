using Domain.Entities;
using Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repository.Implementations
{
    public class LoanRepository : ILoanRepository
    {
        private string _filePath = "C:\\Users\\User\\source\\repos\\atm-simulator\\Repository\\Data\\Loans.json";

        private List<Loan> LoadAll()
        {
            if (!File.Exists(_filePath))
                return new List<Loan>();

            string json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<Loan>();

            return JsonConvert.DeserializeObject<List<Loan>>(json, new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() }
            });
        }

        private void SaveAll(List<Loan> loans)
        {
            string json = JsonConvert.SerializeObject(loans, Formatting.Indented, new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() }
            });
            File.WriteAllText(_filePath, json);
        }

        public void Add(Loan loan)
        {
            var loans = LoadAll();

            if (loan.LoanId == 0)
            {
                int maxId = 0;
                foreach (var l in loans)
                {
                    if (l.LoanId > maxId)
                    {
                        maxId = l.LoanId;
                    }
                }
                loan.LoanId = maxId + 1;
            }

            loans.Add(loan);
            SaveAll(loans);
        }

        public List<Loan> GetAll()
        {
            return LoadAll();
        }

        public Loan GetById(int loanId)
        {
            var loans = LoadAll();
            foreach (var item in loans)
            {
                if (item.LoanId == loanId)
                {
                    return item;
                }
            }
            return null;
        }

        public List<Loan> GetByUserId(int userId)
        {
            var loans = LoadAll();
            var userLoans = new List<Loan>();

            foreach (var item in loans)
            {
                if (item.UserId == userId)
                {
                    userLoans.Add(item);
                }
            }

            return userLoans;
        }

        public void Update(Loan loan)
        {
            var loans = LoadAll();
            for (int i = 0; i < loans.Count; i++)
            {
                if (loans[i].LoanId == loan.LoanId)
                {
                    loans[i] = loan;
                    SaveAll(loans);
                    return;
                }
            }
        }

        public List<Loan> GetPendingLoans()
        {
            return LoadAll()
                .Where(l => l.Status == LoanStatus.Pending)
                .ToList();
        }
    }
}