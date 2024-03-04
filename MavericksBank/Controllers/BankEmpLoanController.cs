using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    public class BankEmpLoanController : Controller
    {
        private readonly ILogger<BankEmpLoanController> _logger;
        private readonly IBankEmpLoanService _service;
        public BankEmpLoanController(ILogger<BankEmpLoanController> logger, IBankEmpLoanService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("ApproveOrDisapproveALoan")]
        [HttpPut]
        public async Task<ActionResult<Loan>> ApproveOrDisapproveLoan(int LID)
        {
            try
            {
                var loan = await _service.ApproveOrDisapproveLoan(LID);
                _logger.LogInformation("Loan Approved/Disapproved");
                return loan;
            }
            catch(NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("ApproveOrDisapproveLoanExtend")]
        [HttpPut]
        public async Task<ActionResult<Loan>> ApproveOrDisapproveLoanExtend(int LID,string approval)
        {
            try
            {
                var loan = await _service.ApproveOrDisapproveLoanExtend(LID,approval);
                _logger.LogInformation("Loan Extend Approved/Disapproved");
                return loan;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("GetAllLoans")]
        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAllLoansApplied()
        {
            try
            {
                var loans = await _service.GetAllLoansApplied();
                _logger.LogInformation("Applied Loans Retrived");
                return loans;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }


        [Authorize(Roles = "Bank Employee,Admin")]
        [HttpGet]
        [Route("GetAllLoanPolicies")]
        public async Task<ActionResult<List<LoanPolicies>>> GetAllLoanPolicies()
        {
            try
            {
                var loanPolicy = await _service.GetDifferentLoanPolicies();
                _logger.LogInformation("Applied Loans Retrived");
                return loanPolicy;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("GetAllLoansbyACustomer")]
        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAllLoansAppliedByACustomer(int CID)
        {
            try
            {
                var loans = await _service.GetAllLoansAppliedByACustomer(CID);
                _logger.LogInformation("Applied Loans Retrived");
                return loans;
            }
            catch (NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee,Admin")]
        [Route("GetAllLoansThatNeedApproval")]
        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAllLoansThatNeedApproval()
        {
            try
            {
                var loans = await _service.GetAllLoansThatNeedApproval();
                _logger.LogInformation("Approval Loans Retrived");
                return loans;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("CheckCustomerCreditworthiness")]
        [HttpGet]
        public async Task<ActionResult<bool>> GetCustomerCreditworthiness(int CID)
        {
            try
            {
                bool creditWorthy = await _service.GetCustomerCreditworthiness(CID);
                _logger.LogInformation("Creditworthiness is checked");
                return creditWorthy;
            }
            catch (NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }

        }
    }
}

