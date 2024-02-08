using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface IBranchAdminService:IBranchUserService
	{
        public Task<BranchCreateDTO> AddBranch(BranchCreateDTO branch);
        public Task<BranchUpdateDTO> UpdateBranch(BranchUpdateDTO branch);
        public Task<Branches> DeleteBranch(string IFSCCode);
    }
}

