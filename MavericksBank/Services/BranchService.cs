using System;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Services
{
	public class BranchService:IBranchAdminService
	{
        private readonly ILogger<BranchService> _logger;
        private readonly IRepository<Branches, string> _BranchRepo;
        public BranchService(ILogger<BranchService> logger, IRepository<Branches, string> BranchRepo)
        {
            _logger = logger;
            _BranchRepo = BranchRepo;
        }

        public async Task<BranchCreateDTO> AddBranch(BranchCreateDTO branch)
        {
            var myBranch = new AddToBranch(branch).GetBranch();
            myBranch = await _BranchRepo.Add(myBranch);
            _logger.LogInformation("Branch Created");
            return branch;
        }

        public async Task<Branches> DeleteBranch(string Key)
        {
            var branch = await _BranchRepo.Delete(Key);
            _logger.LogInformation($"Successfully Deleted Branch : {Key}");
            return branch;
        }

        public async Task<List<Branches>> GetAllBranches()
        {
            var branches = await _BranchRepo.GetAll();
            _logger.LogInformation($"Successfully Retrieved all branches");
            return branches;
        }

        public async Task<List<Branches>> GetAllBranchesByID(int ID)
        {
            var branches = await _BranchRepo.GetAll();
            branches = branches.Where(d => d.BankID == ID).ToList();
            _logger.LogInformation($"Successfully Retrieved all branches");
            return branches;
        }

        public async Task<Branches> GetBranchbyID(string ID)
        {
            var branch = await _BranchRepo.GetByID(ID);
            _logger.LogInformation($"Successfully Retrieved Branch {ID}");
            return branch;
        }

        public async Task<BranchUpdateDTO> UpdateBranch(BranchUpdateDTO branch)
        {

            var myBranch = await _BranchRepo.GetByID(branch.IFSCCode);
            myBranch.BankID = branch.BankID;
            myBranch.BranchName = branch.BranchName;
            myBranch.City = myBranch.City;

            await _BranchRepo.Update(myBranch);
            _logger.LogInformation($"Successfully Updated Branch with ID ");
            return branch;

        }
    }
}

