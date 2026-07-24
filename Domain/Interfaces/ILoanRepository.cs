using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface ILoanRepository
    {
        void Add(Loan loan);
        void Update(Loan loan);
        Loan GetById(int loanId);
        List<Loan> GetAll();
        List<Loan> GetByUserId(int userId);
        List<Loan> GetPendingLoans();
    }
}
