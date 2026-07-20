using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ILoanService
    {
        void LoanRequest(int userId, decimal amount);
        void LoanApprove(int loanid);

        void LoanDecline(int loanId);

        List<Loan> GetAllLoans();

        List<Loan> GetUserLoans(int userId);
    }
}
