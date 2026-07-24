namespace Domain.Entities
{
    public class Transaction
    {
        public string Username { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}