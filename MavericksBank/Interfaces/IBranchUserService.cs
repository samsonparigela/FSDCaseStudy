using System;
using MavericksBank.Models;

namespace MavericksBank.Interfaces
{
	public interface IBranchUserService
	{
        public Task<Branches> GetBranchbyID(string IFSCCode);
        public Task<List<Branches>> GetAllBranches();
        public Task<List<Branches>> GetAllBranchesByID(int ID);

    }
}

