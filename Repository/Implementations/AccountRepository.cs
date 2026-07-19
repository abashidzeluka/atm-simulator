using Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private string _filePath = "C:\\Users\\User\\source\\repos\\atm-simulator\\Repository\\Data\\Accounts.json";

        private List<Account> LoadAll()
        {
            if (!File.Exists(_filePath))
                return new List<Account>();

            string json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<Account>();

            return JsonConvert.DeserializeObject<List<Account>>(json, new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() }
            });
        }

        private void SaveAll(List<Account> accounts)
        {
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented, new JsonSerializerSettings
            {
                Converters = { new StringEnumConverter() }
            });
            File.WriteAllText(_filePath, json);
        }

        public void Add(Account account)
        {
            var accounts = LoadAll();
            accounts.Add(account);
            SaveAll(accounts);
        }

        public Account GetByUsername(string username)
        {
            var accounts = LoadAll();
            foreach (var item in accounts)
            {
                if(item.UserName == username)
                {
                    return item;
                }
            }
            return null;
        }

        public bool Exists(string username)
        {
            var accounts = LoadAll();
            foreach (var item in accounts)
            {
                if(item.UserName == username)
                {
                    return true;
                }
            }
            return false;
        }

        public void Update(Account account)
        {
            var accounts = LoadAll();
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].UserName == account.UserName || accounts[i].Id == account.Id)
                {
                    accounts[i] = account;
                    SaveAll(accounts);
                    return;
                }
            }
        }

        public int Count()
        {
            return LoadAll().Count;
        }
    }
}