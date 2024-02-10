using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLoanController : Controller
    {
        private readonly ILogger<CustomerLoanController> _logger;
        private readonly ICustomerLoanService _service;
        public CustomerLoanController(ILogger<CustomerLoanController> logger, ICustomerLoanService service)
        {
            _logger = logger;
            _service = service;
        }

        [Route("Apply for a loan")]
        [HttpPost]
        public async Task<LoanApplyDTO> ApplyForALoan(LoanApplyDTO ApplyLoan)
        {
            var loan = await _service.ApplyForALoan(ApplyLoan);
            _logger.LogInformation("Applied for a loan");
            return loan;
        }

        [Route("Ask For Extension")]
        [HttpPut]
        public async Task<LoanExtendDTO> AskForExtension(LoanExtendDTO loanExtend)
        {
            var loanExtension = await _service.AskForExtension(loanExtend);
            _logger.LogInformation("Asked for a loan extension");
            return loanExtension;
        }

        [Route("Get All Applied Loans")]
        [HttpGet]
        public async Task<List<Loan>> GetAllAppliedLoans(int ID)
        {
            var loans = await _service.GetAllAppliedLoans(ID);
            _logger.LogInformation("Retrieved all applied loans");
            return loans;
        }

        [Route("Get All Availed Loans")]
        [HttpGet]
        public async Task<List<Loan>> GetAllAvailedLoans(int ID)
        {
            var loans = await _service.GetAllAvailedLoans(ID);
            _logger.LogInformation("Retrieved all Availed loans");
            return loans;
        }

        [Route("Get Different Loan Policies")]
        [HttpGet]
        public async Task<List<LoanPolicies>> GetDifferentLoanPolicies()
        {
            var loanPolicies = await _service.GetDifferentLoanPolicies();
            _logger.LogInformation("Retrieved all loan Policies");
            return loanPolicies;
        }

        [Route("Get Loan By ID")]
        [HttpGet]
        public async Task<Loan> GetLoanByID(int ID)
        {
            var loan = await _service.GetLoanByID(ID);
            _logger.LogInformation("Retrieved loan");
            return loan;
        }

        [Route("Get LoanAmount To Account")]
        [HttpPut]
        public async Task<Accounts> GetLoanAmountToAccount(int LoanID, int AccID)
        {
            var loanAmountTransfer = await _service.GetLoanAmountToAccount(LoanID,AccID);
            _logger.LogInformation("Loan amount credited to account");
            return loanAmountTransfer;
        }

        [Route("Repay Loan")]
        [HttpPut]
        public async Task<Loan> RepayLoan(int loanID, int accountNumber, int amount)
        {
            var loanRepay = await _service.RepayLoan(loanID, accountNumber, amount);
            _logger.LogInformation("Loan Repaid");
            return loanRepay;
        }
    }
}

