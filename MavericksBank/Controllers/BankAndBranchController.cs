using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
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

        [Authorize(Roles = "Bank Employee")]
        [Route("AddBank")]
        [HttpPost]
        public async Task<ActionResult<BankCreateDTO>> AddBankAsync(BankCreateDTO bank)
        {
            try
            {
                var myBank = await _service1.AddBank(bank);
                _logger.LogInformation("Bank Created");
                return myBank;
            }
            catch(NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }

        }


        [Route("GetAllBanks")]
        [HttpGet]
        public async Task<ActionResult<List<Banks>>> GetAllBanksAsync()
        {
            try
            {
                var banks = await _service1.GetAllBanks();
                _logger.LogInformation($"All Banks Retrieved");
                return banks;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("GetBankByID")]
        [HttpGet]
        public async Task<ActionResult<Banks>> GetBankByIDAsync(int ID)
        {
            try
            {
                var bank = await _service1.GetBankbyID(ID);
                _logger.LogInformation($"Bank with ID {ID} Retrieved");
                return bank;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }


        }

        [Authorize(Roles = "Bank Employee")]
        [Route("UpdateBank")]
        [HttpPut]
        public async Task<ActionResult<BankUpdateDTO>> UpdateBankAsync(BankUpdateDTO bank)
        {
            try
            {
                bank = await _service1.UpdateBank(bank);
                _logger.LogInformation("Bank Updated");
                return bank;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }

        }

        [Authorize(Roles = "Bank Employee")]
        [Route("DeleteBank")]
        [HttpDelete]
        public async Task<ActionResult<Banks>> DeleteAccountAsync(int ID)
        {
            try
            {
                var bank = await _service1.DeleteBank(ID);
                _logger.LogInformation("Bank Deleted");
                return bank;
            }
            catch (NoBankFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }

        }

        [Authorize(Roles = "Bank Employee")]
        [Route("AddBranch")]
        [HttpPost]
        public async Task<ActionResult<BranchCreateDTO>> AddBranchAsync(BranchCreateDTO branch)
        {
            try
            {
                var myBranch = await _service2.AddBranch(branch);
                _logger.LogInformation("Branch Created");
                return myBranch;
            }
            catch (NoBranchFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("GetAllBranchByBankID")]
        [HttpGet]
        public async Task<ActionResult<List<Branches>>> GetAllBranchByIDAsync(int ID)
        {
            try
            {
                var branches = await _service2.GetAllBranchesByID(ID);
                _logger.LogInformation($"All Branches of Bank {ID} Retrieved");
                return branches;
            }
            catch (NoBranchFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }

        }

        [Route("GetAllBranches")]
        [HttpGet]
        public async Task<ActionResult<List<Branches>>> GetAllBranchAsync()
        {
            try
            {
                var branches = await _service2.GetAllBranches();
                _logger.LogInformation($"All Branches in the DB Retrieved");
                return branches;
            }
            catch (NoBranchFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }

        }

        [Route("GetBranchByID")]
        [HttpGet]
        public async Task<ActionResult<Branches>> GetBranchByIDAsync(string ID)
        {
            try
            {
                var branch = await _service2.GetBranchbyID(ID);
                _logger.LogInformation($"Branch with ID {ID} Retrieved");
                return branch;
            }
            catch (NoBranchFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee")]
        [Route("UpdateBranch")]
        [HttpPut]
        public async Task<ActionResult<BranchUpdateDTO>> UpdateBranchAsync(BranchUpdateDTO branch)
        {
            try
            {
                branch = await _service2.UpdateBranch(branch);
                _logger.LogInformation("Branch Updated");
                return branch;
            }
            catch (NoBranchFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Bank Employee")]
        [Route("DeleteBranch")]
        [HttpDelete]
        public async Task<ActionResult<Branches>> DeleteBranchAsync(string ID)
        {
            try
            {
                var branch = await _service2.DeleteBranch(ID);
                _logger.LogInformation("Branch Deleted");
                return branch;
            }
            catch (NoBranchFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

    }
}

