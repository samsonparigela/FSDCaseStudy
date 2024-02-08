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
    public class BranchController : Controller
    {
        private readonly ILogger<BranchController> _logger;
        private readonly IBranchAdminService _service;
        public BranchController(ILogger<BranchController> logger, IBranchAdminService service)
        {
            _logger = logger;
            _service = service;
        }


        [Route("Add")]
        [HttpPost]
        public async Task<BranchCreateDTO> AddBranchAsync(BranchCreateDTO branch)
        {
            var myBranch = await _service.AddBranch(branch);
            _logger.LogInformation("Branch Created");
            return myBranch;
        }


        [Route("GetAllBranchByBankID")]
        [HttpGet]
        public async Task<List<Branches>> GetAllBranchByIDAsync(int ID)
        {
            var branches = await _service.GetAllBranchesByID(ID);
            _logger.LogInformation($"All Branches of Bank {ID} Retrieved");
            return branches;
        }

        [Route("GetAllBranches")]
        [HttpGet]
        public async Task<List<Branches>> GetAllBranchAsync()
        {
            var branches = await _service.GetAllBranches();
            _logger.LogInformation($"All Branches in the DB Retrieved");
            return branches;
        }

        [Route("GetBranchByID")]
        [HttpGet]
        public async Task<Branches> GetBranchByIDAsync(string ID)
        {
            var branch = await _service.GetBranchbyID(ID);
            _logger.LogInformation($"Branch with ID {ID} Retrieved");
            return branch;
        }

        [Route("Update")]
        [HttpPut]
        public async Task<BranchUpdateDTO> UpdateBranchAsync(BranchUpdateDTO branch)
        {
            branch = await _service.UpdateBranch(branch);
            _logger.LogInformation("Branch Updated");
            return branch;
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<Branches> DeleteBranchAsync(string ID)
        {
            var branch = await _service.DeleteBranch(ID);
            _logger.LogInformation("Branch Deleted");
            return branch;
        }
    }
}

