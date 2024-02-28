using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    public class CustomerLoanController : Controller
    {
        private readonly ILogger<CustomerLoanController> _logger;
        private readonly ICustomerLoanService _service;
        public CustomerLoanController(ILogger<CustomerLoanController> logger, ICustomerLoanService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize(Roles = "Customer")]
        [Route("ApplyForALoan")]
        [HttpPost]
        public async Task<ActionResult<LoanApplyDTO>> ApplyForALoan(LoanApplyDTO ApplyLoan)
        {
            try
            {
                var loan = await _service.ApplyForALoan(ApplyLoan);
                _logger.LogInformation("Applied for a loan");
                return loan;
            }
            catch(LoanNotApprovedYetException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("AskForExtension")]
        [HttpPut]
        public async Task<ActionResult<LoanExtendDTO>> AskForExtension(LoanExtendDTO loanExtend)
        {
            try
            {
                var loanExtension = await _service.AskForExtension(loanExtend);
                _logger.LogInformation("Asked for a loan extension");
                return loanExtension;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("GetAllAppliedLoans")]
        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAllAppliedLoans(int ID)
        {
            try
            {
                var loans = await _service.GetAllAppliedLoans(ID);
                _logger.LogInformation("Retrieved all applied loans");
                return loans;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("GetAllAvailedLoans")]
        [HttpGet]
        public async Task<ActionResult<List<Loan>>> GetAllAvailedLoans(int ID)
        {
            try
            {
                var loans = await _service.GetAllAvailedLoans(ID);
                _logger.LogInformation("Retrieved all Availed loans");
                return loans;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("GetDifferentLoanPolicies")]
        [HttpGet]
        public async Task<ActionResult<List<LoanPolicies>>> GetDifferentLoanPolicies()
        {
            try
            {
                var loanPolicies = await _service.GetDifferentLoanPolicies();
                _logger.LogInformation("Retrieved all loan Policies");
                return loanPolicies;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("GetLoanByID")]
        [HttpGet]
        public async Task<ActionResult<Loan>> GetLoanByID(int ID)
        {
            try
            {
                var loan = await _service.GetLoanByID(ID);
                _logger.LogInformation("Retrieved loan");
                return loan;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("GetLoanAmountToAccount")]
        [HttpPut]
        public async Task<ActionResult<Accounts>> GetLoanAmountToAccount(int LoanID, int AccID)
        {
            try
            {
                var loanAmountTransfer = await _service.GetLoanAmountToAccount(LoanID, AccID);
                _logger.LogInformation("Loan amount credited to account");
                return loanAmountTransfer;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("RepayLoan")]
        [HttpPut]
        public async Task<ActionResult<Loan>> RepayLoan(int loanID, int accountNumber, int amount)
        {
            try
            {
                var loanRepay = await _service.RepayLoan(loanID, accountNumber, amount);
                _logger.LogInformation("Loan Repaid");
                return loanRepay;
            }
            catch (NoLoanFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

