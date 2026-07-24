using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private string _filePath = "C:\\Users\\User\\source\\repos\\atm-simulator\\Repository\\Data\\Users.json";

        public UserRepository()
        {
            SeedAdmin();
        }

        private List<User> LoadAll()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<User>();

                string json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                    return new List<User>();

                return JsonConvert.DeserializeObject<List<User>>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                    Converters = { new StringEnumConverter() }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading accounts: {ex.Message}");
                return new List<User>();
            }
        }

        private void SaveAll(List<User> accounts)
        {
            try
            {
                string json = JsonConvert.SerializeObject(accounts, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                    Converters = { new StringEnumConverter() }
                });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving accounts: {ex.Message}");
            }
        }

        public void Add(User account)
        {
            var accounts = LoadAll();
            accounts.Add(account);
            SaveAll(accounts);
        }

        public User GetByUsername(string username)
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

        public void Update(User account)
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

        private void SeedAdmin()
        {
            if (!Exists("Admin"))
            {
                Admin admin = new Admin(0, "Admin", Hasher.Hash("Admin123"));
                Add(admin);
            }
        }

        public User GetById(int id)
        {
            var users = LoadAll();
            foreach (var user in users)
            {
                if (user.Id == id)
                    return user;
            }
            return null;
        }

        public int Count()
        {
            return LoadAll().Count;
        }
    }
}