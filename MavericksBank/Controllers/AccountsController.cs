using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly IAccountAdminService _service;
        public AccountsController(ILogger<AccountsController> logger, IAccountAdminService service)
        {
            _logger = logger;
            _service = service;
        }


        [Route("Add")]
        [HttpPost]
        public async Task<AccountsCreateDTO> AddAccount(AccountsCreateDTO account)
        {
            var myAccount = await _service.AddAccount(account);
            _logger.LogInformation("Account Created");
            return myAccount;
        }


        [Route("GetAllAccountsByCustomerID")]
        [HttpGet]
        public async Task<List<Accounts>> GetAllByID(int ID)
        {
            var accounts = await _service.GetAllAccountsByUserID(ID);
            _logger.LogInformation($"All Accounts of Customer {ID} Retrieved");
            return accounts;
        }

        [Route("GetAllAccounts")]
        [HttpGet]
        public async Task<List<Accounts>> GetAllAccounts()
        {
            var accounts = await _service.GetAllAccounts();
            _logger.LogInformation($"All Accounts in the DB Retrieved");
            return accounts;
        }

        [Route("GetByID")]
        [HttpGet]
        public async Task<Accounts> GetAccountByIDAsync(int ID)
        {
            var employee = await _service.GetAccountByID(ID);
            _logger.LogInformation($"Account with ID {ID} Retrieved");
            return employee;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<AccountsUpdateDTO> UpdateAccountsAsync(AccountsUpdateDTO accounts)
        {
            accounts = await _service.UpdateAccount(accounts);
            _logger.LogInformation("Account Updated");
            return accounts;
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<Accounts> DeleteAccountAsync(int ID)
        {
            var account = await _service.DeleteAccount(ID);
            _logger.LogInformation("Account Deleted");
            return account;
        }
    }
}

