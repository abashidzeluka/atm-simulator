using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public LoanStatus Status { get; set; }
        public DateTime RequestDate { get; set; }

        public Loan(int userId, decimal amount)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be greater than zero.");

            if (amount <= 0)
                throw new ArgumentException("Loan amount must be greater than zero.");

            UserId = userId;
            Amount = amount;
            Status = LoanStatus.Pending;
            RequestDate = DateTime.Now;
        }
    }
}