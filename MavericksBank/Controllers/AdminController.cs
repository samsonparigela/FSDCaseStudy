using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _service;

        public AdminController(ILogger<AdminController> logger, IAdminService service)
        {
            _logger = logger;
            _service = service;

        }

        #region Register & Login
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<ActionResult<LoginDTO>> Register(string userName, string password)
        {
            try
            {
                var customer = await _service.Register(userName, password);
                _logger.LogInformation("Successfully Registered");
                return customer;
            }
            catch (UserExistsException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<LoginDTO>> Login(LoginDTO customer)
        {
            try
            {
                customer = await _service.Login(customer);
                _logger.LogInformation("Successfully LoggedIn");
                return customer;
            }
            catch (InvalidUserException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("GetAdminByID")]
        [HttpGet]
        public async Task<ActionResult<Admin>> GetAdminByID(int key)
        {
            try
            {
                var admin = await _service.GetAdminByID(key);
                _logger.LogInformation("Successfully LoggedIn");
                return admin;
            }
            catch (InvalidUserException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        #endregion

        #region Customer

        #endregion

        #region BEAccounts

        #endregion

        #region BELoans

        [Authorize(Roles = "Admin")]
        [Route("AddLoanPolicies")]
        [HttpPost]
        public async Task<ActionResult<LoanPolicyDTO>> AddLoanPolicies(LoanPolicyDTO policies)
        {
            try
            {
                var policy = await _service.AddLoanPolicies(policies);
                _logger.LogInformation($"Loan Policy Added");
                return policy;
            }
            catch (NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("DeleteLoanPolicies")]
        [HttpDelete]
        public async Task<ActionResult<LoanPolicies>> DeleteLoanPolicies(int ID)
        {
            try
            {
                var policy = await _service.DeleteLoanPolicies(ID);
                _logger.LogInformation($"Loan Policy Delete");
                return policy;
            }
            catch (NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("UpdateLoanPolicies")]
        [HttpPut]
        public async Task<ActionResult<LoanPolicies>> UpdateLoanPolicies(LoanPolicies policies)
        {
            try
            {
                var policy = await _service.UpdateLoanPolicies(policies);
                _logger.LogInformation($"5 Highest amount transactions retrieved");
                return policy;
            }
            catch (NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        #endregion

        #region Bank

        #endregion

        #region Branch

        #endregion

        #region CustAccount

        #endregion

        #region CustBenif

        [Route("DeleteBeneficiary")]
        [HttpDelete]
        public async Task<ActionResult<Beneficiaries>> DeleteBeneficiary(int ID)
        {
            try
            {
                var benif = await _service.DeleteBeneficiary(ID);
                _logger.LogInformation($"benif Deleted");
                return benif;
            }
            catch (NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }


        #endregion

        #region CustLoan

        #endregion

        #region CustTransac

        #endregion



    }
}


