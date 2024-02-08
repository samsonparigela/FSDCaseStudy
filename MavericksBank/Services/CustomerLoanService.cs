﻿using System;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Services
{
	public class CustomerLoanService:ICustomerLoanService
	{
		private readonly ILogger<CustomerLoanService> _logger;
		private readonly IRepository<Loan, int> _loanRepo;
        private readonly IRepository<Accounts, int> _AccRepo;
        private readonly IRepository<LoanPolicies, int> _loanPolicyRepo;
        private readonly IAccountAdminService _accountsService;

        public CustomerLoanService(ILogger<CustomerLoanService> logger, IRepository<Loan, int> loanRepo,
			IRepository<LoanPolicies, int> loanPolicyRepo, IAccountAdminService accountsService, IRepository<Accounts, int> AccRepo)
		{
			_logger = logger;
			_loanRepo = loanRepo;
			_loanPolicyRepo = loanPolicyRepo;
            _accountsService = accountsService;
            _AccRepo = AccRepo;
		}

        public async Task<LoanApplyDTO> ApplyForALoan(LoanApplyDTO ApplyLoan)
        {
            var loan = new AddToLoan(ApplyLoan).GetLoan();
            await _loanRepo.Add(loan);
            _logger.LogInformation("Loan is Applied Successfully");
            return ApplyLoan;
        }

        public async Task<LoanExtendDTO> AskForExtension(LoanExtendDTO loanExtend)
        {
            var loan = await _loanRepo.GetByID(loanExtend.LoanID);
            loan.TenureInMonths=loanExtend.TenureInMonths;
            loan.Status = loanExtend.Status;
            loan = await _loanRepo.Update(loan);
            _logger.LogInformation("Loan is Asked for Extension Successfully");
            return loanExtend;
        }

        public async Task<List<Loan>> GetAllAppliedLoans(int ID)
        {
            var loans = await _loanRepo.GetAll();
            loans= loans.Where(d => d.CustomerID == ID).ToList();
            _logger.LogInformation("All Apllied Loans retrieved Successfully");
            return loans;
        }

        public async Task<List<Loan>> GetAllAvailedLoans(int ID)
        {
            var loans = await _loanRepo.GetAll();
            loans = loans.Where(d => d.CustomerID == ID && d.Status!="Pending").ToList();
            _logger.LogInformation("All Availed Loans retrieved Successfully");
            return loans;
        }

        public async Task<List<LoanPolicies>> GetDifferentLoanPolicies()
        {
            var loanPolicies = await _loanPolicyRepo.GetAll();
            _logger.LogInformation("All Loan Policies retrieved Successfully");
            return loanPolicies;
        }

        public async Task<Loan> GetLoanByID(int ID)
        {
            var loan = await _loanRepo.GetByID(ID);
            _logger.LogInformation($"Loan {ID} retrieved Successfully");
            return loan;
        }

        public async Task<Accounts> GetLoanAmountToAccount(int LoanID,int AccID)
        {
            var loan = await _loanRepo.GetByID(LoanID);
            var account = await _accountsService.GetAccountByID(AccID);
            if (loan.Status == "Approved")
            {
                account.Balance += loan.LoanAmount;
                account = await _AccRepo.Update(account);
            }
            _logger.LogInformation($"Loan Amount Added to Account Successfully");
            return account;
        }

        public async Task<Loan> RepayLoan(int loanID,string accountType,int amount)
        {
            var loan = await _loanRepo.GetByID(loanID);
            var customerID = loan.CustomerID;
            var account = await _accountsService.GetAllAccountsByUserID(customerID);
            var account2 = account.SingleOrDefault(a => a.AccountType == accountType);
            account2.Balance -= amount;
            loan.LoanAmount -= amount;
            if(loan.LoanAmount==0)
            {
                loan.Status = "Repayed";
            }
            return loan;
        }
    }
}

