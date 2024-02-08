using System;
using System.Security.Cryptography;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Services;

namespace MavericksBank.Repository
{
	public class BankEmpLoanService: IBankEmpLoanService
    {
        private readonly ILogger<BankEmpLoanService> _logger;
        private readonly IRepository<Loan, int> _LoanRepo;
        private readonly ICustomerAccountService _AccService;
        private readonly ICustomerLoanService _LoanService;
        private readonly IRepository<Customer, int> _CustRepo;

        public BankEmpLoanService(ILogger<BankEmpLoanService> logger, IRepository<Loan, int> LoanRepo,
            ICustomerAccountService AccService, IRepository<Customer, int> CustRepo, ICustomerLoanService LoanService)
		{
            _logger = logger;
            _LoanRepo = LoanRepo;
            _AccService = AccService;
            _CustRepo = CustRepo;
            _LoanService = LoanService;
		}

        public async Task<Loan> ApproveOrDisapproveLoan(int LID)
        {
            var loan = await _LoanRepo.GetByID(LID);
            if(await GetCustomerCreditworthiness(loan.CustomerID))
            {
                loan.Status = "Approved";
            }
            else
            {
                loan.Status = "Disapproved";
            }
            loan = await _LoanRepo.Update(loan);
            return loan;
        }

        public async Task<List<Loan>> GetAllLoansApplied()
        {
            var loans = await _LoanRepo.GetAll();
            return loans;
        }

        public async Task<List<Loan>> GetAllLoansAppliedByACustomer(int CID)
        {
            var loans = await _LoanRepo.GetAll();
            var Customerloans = loans.Where(l => l.CustomerID == CID).ToList();
            return Customerloans;
        }

        public async Task<List<Loan>> GetAllLoansThatNeedApproval()
        {
            var loans = await _LoanRepo.GetAll();
            var Approvalloans = loans.Where(l => l.Status=="Pending").ToList();
            return Approvalloans;
        }

        public async Task<bool> GetCustomerCreditworthiness(int CID)
        {
            var transactions =await _AccService.ViewAllYourTransactions(CID);
            var loans = await _LoanService.GetAllAppliedLoans(CID);
            int transacCount = transactions.Count();
            int loanCount = loans.Count();
            if (transacCount > 2 && loanCount < 2)
                return true;
            return false;
        }
    }
}

