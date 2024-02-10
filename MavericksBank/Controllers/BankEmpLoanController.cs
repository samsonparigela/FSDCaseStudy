using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankEmpLoanController : Controller
    {
        private readonly ILogger<BankEmpLoanController> _logger;
        private readonly IBankEmpLoanService _service;
        public BankEmpLoanController(ILogger<BankEmpLoanController> logger, IBankEmpLoanService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("Approve or Disapprove a Loan")]
        [HttpPut]
        public async Task<Loan> ApproveOrDisapproveLoan(int LID)
        {
            var loan = await _service.ApproveOrDisapproveLoan(LID);
            _logger.LogInformation("Loan Approved/Disapproved");
            return loan;
        }

        [Route("Get all Loans")]
        [HttpGet]
        public async Task<List<Loan>> GetAllLoansApplied()
        {
            var loans = await _service.GetAllLoansApplied();
            _logger.LogInformation("Applied Loans Retrived");
            return loans;
        }

        [Route("Get all LoanPolicies")]
        [HttpGet]
        public async Task<List<LoanPolicies>> GetAllLoanPolicies()
        {
            var loanPolicy = await _service.GetDifferentLoanPolicies();
            _logger.LogInformation("Applied Loans Retrived");
            return loanPolicy;
        }

        [Route("Add Loan Policies")]
        [HttpPost]
        public async Task<LoanPolicies> AddLoanPolicies(LoanPolicies policies)
        {
            var loanPolicy = await _service.AddLoanPolicies(policies);
            _logger.LogInformation("Added Loan Policy");
            return loanPolicy;
        }

        [Route("Delete Loan Policies")]
        [HttpDelete]
        public async Task<LoanPolicies> DeleteLoanPolicies(int ID)
        {
            var loanPolicy = await _service.DeleteLoanPolicies(ID);
            _logger.LogInformation("Deleted Loan Policy");
            return loanPolicy;
        }

        [Route("Update Loan Policies")]
        [HttpPut]
        public async Task<LoanPolicies> UpdateLoanPolicies(LoanPolicies policies)
        {
            var loanPolicy = await _service.UpdateLoanPolicies(policies);
            _logger.LogInformation("Updated Loan Policy");
            return loanPolicy;
        }


        [Route("Get all Loans by a Customer")]
        [HttpGet]
        public async Task<List<Loan>> GetAllLoansAppliedByACustomer(int CID)
        {
            var loans = await _service.GetAllLoansAppliedByACustomer(CID);
            _logger.LogInformation("Applied Loans Retrived");
            return loans;
        }

        [Route("Get all Loans that need approval")]
        [HttpGet]
        public async Task<List<Loan>> GetAllLoansThatNeedApproval()
        {
            var loans = await _service.GetAllLoansThatNeedApproval();
            _logger.LogInformation("Approval Loans Retrived");
            return loans;
        }

        [Route("Check customer Creditworthiness")]
        [HttpGet]
        public async Task<bool> GetCustomerCreditworthiness(int CID)
        {
            bool creditWorthy = await _service.GetCustomerCreditworthiness(CID);
            _logger.LogInformation("Creditworthiness is checked");
            return creditWorthy;

        }
    }
}

