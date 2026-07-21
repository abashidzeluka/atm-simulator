using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public void LoanRequest(int userId, decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Loan amount must be greater than zero.");
            }

            var loan = new Loan(userId, amount)
            {
                UserId = userId,
                Amount = amount,
                Status = LoanStatus.Pending
            };

            _loanRepository.Add(loan);
        }

        public List<Loan> GetUserLoans(int userId)
        {
            return _loanRepository.GetByUserId(userId);
        }

        public List<Loan> GetAllLoans()
        {
            return _loanRepository.GetAll();
        }

        public void LoanApprove(int loanId)
        {
            var loan = _loanRepository.GetById(loanId);
            if (loan == null)
            {
                throw new KeyNotFoundException($"Loan with ID {loanId} was not found.");
            }

            loan.Status = LoanStatus.Approved;
            _loanRepository.Update(loan);
        }

        public void LoanDecline(int loanId)
        {
            var loan = _loanRepository.GetById(loanId);
            if (loan == null)
            {
                throw new KeyNotFoundException($"Loan with ID {loanId} was not found.");
            }

            loan.Status = LoanStatus.Declined;
            _loanRepository.Update(loan);
        }

        public List<Loan> GetPendingLoans()
        {
            return _loanRepository.GetPendingLoans();
        }
    }
}