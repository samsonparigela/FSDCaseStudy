using System;
using System.Security.Cryptography;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Services;

namespace MavericksBank.Repository
{
    public class BankEmpLoanService : IBankEmpLoanService
    {
        private readonly ILogger<BankEmpLoanService> _logger;
        private readonly IRepository<Loan, int> _LoanRepo;
        private readonly IRepository<LoanPolicies, int> _LoanPolicyRepo;
        private readonly ICustomerAccountService _AccService;
        private readonly ICustomerLoanService _LoanService;
        private readonly IRepository<Customer, int> _CustRepo;

        public BankEmpLoanService(ILogger<BankEmpLoanService> logger, IRepository<Loan, int> LoanRepo,
            ICustomerAccountService AccService, IRepository<Customer, int> CustRepo,
            ICustomerLoanService LoanService, IRepository<LoanPolicies, int> LoanPolicyRepo)
        {
            _logger = logger;
            _LoanRepo = LoanRepo;
            _AccService = AccService;
            _CustRepo = CustRepo;
            _LoanService = LoanService;
            _LoanPolicyRepo = LoanPolicyRepo;
        }

        public async Task<Loan> ApproveOrDisapproveLoan(int LID)
        {
            var loan = await _LoanRepo.GetByID(LID);
            if (await GetCustomerCreditworthiness(loan.CustomerID))
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

        public async Task<Loan> ApproveOrDisapproveLoanExtend(int LID,string approval)
        {
            var loan = await _LoanRepo.GetByID(LID);
            if(approval=="Approve Extension")
            loan.Status = "Deposited";
            loan = await _LoanRepo.Update(loan);
            return loan;
        }

        public async Task<List<Loan>> GetAllLoansApplied()
        {
            var loans = await _LoanRepo.GetAll();
            return loans;
        }


        public async Task<List<LoanPolicies>> GetDifferentLoanPolicies()
        {
            var loanPolicies = await _LoanPolicyRepo.GetAll();
            _logger.LogInformation("All Loan Policies retrieved Successfully");
            return loanPolicies;
        }

        public async Task<LoanPolicies> AddLoanPolicies(LoanPolicies policies)
        {
            var loanPolicies = await _LoanPolicyRepo.Add(policies);
            _logger.LogInformation("Loan Policy added Successfully");
            return loanPolicies;
        }

        public async Task<LoanPolicies> DeleteLoanPolicies(int ID)
        {
            var loanPolicies = await _LoanPolicyRepo.Delete(ID);
            _logger.LogInformation("Loan Policy Deleted Successfully");
            return loanPolicies;
        }

        public async Task<LoanPolicies> UpdateLoanPolicies(LoanPolicies policies)
        {

            var loanPolicy = await _LoanPolicyRepo.GetByID(policies.LoanPolicyID);
            loanPolicy.Interest = policies.Interest;
            loanPolicy.LoanAmount = policies.LoanAmount;
            loanPolicy.TenureInMonths = policies.TenureInMonths;
            loanPolicy = await _LoanPolicyRepo.Update(loanPolicy);
            _logger.LogInformation("Loan Policy Updated Successfully");
            return loanPolicy;
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
            var Approvalloans = loans.Where(l => l.Status == "Pending").ToList();
            return Approvalloans;
        }

        public async Task<bool> GetCustomerCreditworthiness(int CID)
        {
            var transactions = await _AccService.ViewAllYourTransactions(CID);
            var inbound = transactions.Where(t => t.TransactionType == "Deposit" || t.TransactionType == "Withdraw").ToList().Count();
            var outbound = transactions.Where(t => t.TransactionType == "Sent").ToList().Count();
            float ratio=0;
            if(outbound!=0)
                ratio = inbound / outbound;
            var loans = await _LoanService.GetAllAppliedLoans(CID);
            int transacCount = transactions.Count();
            int loanCount = loans.Count();
            if (transacCount > 2 && ratio > 1 )
                return true;
            return false;
        }
    }
}

