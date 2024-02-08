using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Mappers
{
	public class AddToBranch
	{
        Branches branch;
        public AddToBranch(BranchCreateDTO branchCreate)
        {
            branch = new Branches();
            branch.BankID = branchCreate.BankID;
            branch.BranchName = branchCreate.BranchName;
            branch.City = branchCreate.City;
            branch.IFSCCode = branchCreate.IFSCCode;
        }
        public Branches GetBranch()
        {
            return branch;
        }
    }
}

