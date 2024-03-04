using System;
using System.Security.Cryptography;
using System.Text;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Mappers;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MavericksBank.Services
{
    public class AdminService : IAdminService
    {

        #region Initializations
        private readonly ILogger<AdminService> _logger;
        private readonly IRepository<Customer, int> _CustomerRepo;
        private readonly IRepository<Admin, int> _AdminRepo;
        private readonly IRepository<Banks, int> _BankRepo;
        private readonly IRepository<Branches, string> _BranchesRepo;
        private readonly IRepository<BankEmployee, int> _BankEmpRepo;
        private readonly IRepository<Users, string> _UserRepo;
        private readonly IRepository<Accounts, int> _AccRepo;
        private readonly IRepository<Transactions, int> _TransacRepo;
        private readonly IRepository<Beneficiaries, int> _BenefRepo;
        private readonly IRepository<Loan, int> _loanRepo;
        private readonly IRepository<LoanPolicies, int> _loanPolicyRepo;
        private readonly ITokenService _tokenService;

        public AdminService(ILogger<AdminService> logger, IRepository<Customer, int> CustomerRepo, IRepository<Admin, int> AdminRepo,
            IRepository<Banks, int> BankRepo, IRepository<Branches, string> BranchesRepo, IRepository<BankEmployee, int> BankEmpRepo,
            IRepository<Users, string> UserRepo, IRepository<Accounts, int> AccRepo, IRepository<Transactions, int> TransacRepo,
            IRepository<Beneficiaries, int> BenefRepo, IRepository<Loan, int> loanRepo, IRepository<LoanPolicies, int> loanPolicyRepo,
            ITokenService tokenService)
        {
            _logger = logger;
            _CustomerRepo = CustomerRepo;
            _AdminRepo = AdminRepo;
            _BankEmpRepo = BankEmpRepo;
            _BankRepo = BankRepo;
            _BranchesRepo = BranchesRepo;
            _UserRepo = UserRepo;
            _AccRepo = AccRepo;
            _TransacRepo = TransacRepo;
            _BenefRepo = BenefRepo;
            _loanPolicyRepo = loanPolicyRepo;
            _loanRepo = loanRepo;
            _tokenService = tokenService;

        }

        #endregion

        #region Register & Login
        public async Task<LoginDTO> Register(string name, string password)
        {
            var x = await _UserRepo.GetByID(name);
            if (x != null)
            {
                throw new UserExistsException("User Already Exists");
            }
            var myUser = new RegisterToAdmin(name, password).GetUser();
            myUser = await _UserRepo.Add(myUser);

            var admin = new Admin
            {
                UserID = myUser.UserID,
                Name = name
            };

            var myAdmin = await _AdminRepo.Add(admin);

            LoginDTO result = new LoginDTO
            {
                UserName = myUser.UserName,
                UserType = "Admin",
            };
            _logger.LogInformation("AdminSuccessfully Registered");
            return result;
        }


        public async Task<LoginDTO> Login(LoginDTO adminLogin)
        {
            var user = await _UserRepo.GetByID(adminLogin.UserName);
            var admins = await _AdminRepo.GetAll();
            var admin = admins.Where(c => c.UserID == user.UserID).ToList().SingleOrDefault();

            if (user == null)
                throw new InvalidUserException();
            var password = GetEncryptedPassword(adminLogin.Password, user.Key);
            if (ComparePasswords(password, user.Password))
            {
                adminLogin.Password = "";
                adminLogin.UserType = user.UserType;
                adminLogin.token = await _tokenService.GenerateToken(adminLogin);
                adminLogin.userID = admin.AdminID;
            }
            else
                throw new InvalidUserException();
            _logger.LogInformation("Successfully Logged in");
            return adminLogin;
        }

        public byte[] GetEncryptedPassword(String password, byte[] key)
        {
            HMACSHA512 hmac = new HMACSHA512(key);
            byte[] userPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return userPassword;
        }

        public bool ComparePasswords(byte[] pwd1, byte[] pwd2)
        {
            for (int i = 0; i < pwd1.Length; i++)
            {
                if (pwd1[i] != pwd2[i])
                    return false;
            }
            return true;
        }

        public async Task<Admin> GetAdminByID(int key)
        {
            var admin = await _AdminRepo.GetByID(key);
            _logger.LogInformation($"Successfully Retrieved Admin with ID : {key}");
            return admin;
        }
        #endregion

        #region Customer

        #endregion

        #region BELoans

        public async Task<LoanPolicyDTO> AddLoanPolicies(LoanPolicyDTO policies)
        {
            var policy = new LoanPolicies
            {
                Interest = policies.Interest,
                LoanAmount = policies.LoanAmount,
                TenureInMonths = policies.TenureInMonths
            };

            var loanPolicies = await _loanPolicyRepo.Add(policy);
            policies.LoanPolicyID = loanPolicies.LoanPolicyID;
            _logger.LogInformation("Loan Policy added Successfully");
            return policies;
        }

        public async Task<LoanPolicies> DeleteLoanPolicies(int ID)
        {
            var loanPolicies = await _loanPolicyRepo.Delete(ID);
            _logger.LogInformation("Loan Policy Deleted Successfully");
            return loanPolicies;
        }

        public async Task<LoanPolicies> UpdateLoanPolicies(LoanPolicies policies)
        {

            var loanPolicy = await _loanPolicyRepo.GetByID(policies.LoanPolicyID);
            loanPolicy.Interest = policies.Interest;
            loanPolicy.LoanAmount = policies.LoanAmount;
            loanPolicy.TenureInMonths = policies.TenureInMonths;
            loanPolicy = await _loanPolicyRepo.Update(loanPolicy);
            _logger.LogInformation("Loan Policy Updated Successfully");
            return loanPolicy;
        }

        #endregion

        #region BankEmp

        #endregion

        #region Bank

        #endregion

        #region Branch

        #endregion

        #region CustAccount

        #endregion

        #region CustBenef

        public async Task<Beneficiaries> DeleteBeneficiary(int ID)
        {
            var benificiary = await _BenefRepo.Delete(ID);
            _logger.LogInformation("Beneficiary Deleted");
            return benificiary;

        }

        #endregion

        #region CustLoan

        #endregion

        #region CustTransac

        #endregion

    }
}

