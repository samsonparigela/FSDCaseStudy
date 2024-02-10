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
    public class BankAndBranchController : ControllerBase
    {
        private readonly ILogger<BankAndBranchController> _logger;
        private readonly IBankAdminService _service1;
        private readonly IBranchAdminService _service2;
        public BankAndBranchController(ILogger<BankAndBranchController> logger,
            IBankAdminService service1, IBranchAdminService service2)
        {
            _logger = logger;
            _service1 = service1;
            _service2 = service2;
        }


        [Route("Add Bank")]
        [HttpPost]
        public async Task<BankCreateDTO> AddBankAsync(BankCreateDTO bank)
        {
            var myBank = await _service1.AddBank(bank);
            _logger.LogInformation("Bank Created");
            return myBank;
        }


        [Route("GetAllBanks")]
        [HttpGet]
        public async Task<List<Banks>> GetAllBanksAsync()
        {
            var banks = await _service1.GetAllBanks();
            _logger.LogInformation($"All Banks Retrieved");
            return banks;
        }

        [Route("GetBankByID")]
        [HttpGet]
        public async Task<Banks> GetBankByIDAsync(int ID)
        {
            var bank = await _service1.GetBankbyID(ID);
            _logger.LogInformation($"Bank with ID {ID} Retrieved");
            return bank;
        }

        [Route("Update Bank")]
        [HttpPut]
        public async Task<BankUpdateDTO> UpdateBankAsync(BankUpdateDTO bank)
        {
            bank = await _service1.UpdateBank(bank);
            _logger.LogInformation("Bank Updated");
            return bank;
        }

        [Route("Delete Bank")]
        [HttpDelete]
        public async Task<Banks> DeleteAccountAsync(int ID)
        {
            var bank = await _service1.DeleteBank(ID);
            _logger.LogInformation("Bank Deleted");
            return bank;
        }

        [Route("Add Branch")]
        [HttpPost]
        public async Task<BranchCreateDTO> AddBranchAsync(BranchCreateDTO branch)
        {
            var myBranch = await _service2.AddBranch(branch);
            _logger.LogInformation("Branch Created");
            return myBranch;
        }

        [Route("GetAllBranchByBankID")]
        [HttpGet]
        public async Task<List<Branches>> GetAllBranchByIDAsync(int ID)
        {
            var branches = await _service2.GetAllBranchesByID(ID);
            _logger.LogInformation($"All Branches of Bank {ID} Retrieved");
            return branches;
        }

        [Route("GetAllBranches")]
        [HttpGet]
        public async Task<List<Branches>> GetAllBranchAsync()
        {
            var branches = await _service2.GetAllBranches();
            _logger.LogInformation($"All Branches in the DB Retrieved");
            return branches;
        }

        [Route("GetBranchByID")]
        [HttpGet]
        public async Task<Branches> GetBranchByIDAsync(string ID)
        {
            var branch = await _service2.GetBranchbyID(ID);
            _logger.LogInformation($"Branch with ID {ID} Retrieved");
            return branch;
        }

        [Route("Update Branch")]
        [HttpPut]
        public async Task<BranchUpdateDTO> UpdateBranchAsync(BranchUpdateDTO branch)
        {
            branch = await _service2.UpdateBranch(branch);
            _logger.LogInformation("Branch Updated");
            return branch;
        }

        [Route("Delete Branch")]
        [HttpDelete]
        public async Task<Branches> DeleteBranchAsync(string ID)
        {
            var branch = await _service2.DeleteBranch(ID);
            _logger.LogInformation("Branch Deleted");
            return branch;
        }

    }
}

