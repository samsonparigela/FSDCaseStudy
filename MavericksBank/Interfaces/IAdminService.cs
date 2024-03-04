using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Interfaces
{
	public interface IAdminService
	{
        public Task<LoginDTO> Register(string name, string password);
        public Task<LoginDTO> Login(LoginDTO adminLogin);
        public byte[] GetEncryptedPassword(String password, byte[] key);
        public bool ComparePasswords(byte[] pwd1, byte[] pwd2);
        public Task<Admin> GetAdminByID(int key);

        public Task<Beneficiaries> DeleteBeneficiary(int ID);

        public Task<LoanPolicyDTO> AddLoanPolicies(LoanPolicyDTO policies);
        public Task<LoanPolicies> DeleteLoanPolicies(int ID);
        public Task<LoanPolicies> UpdateLoanPolicies(LoanPolicies policies);


    }
}

