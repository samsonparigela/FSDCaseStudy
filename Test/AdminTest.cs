using System;
using System.Security.Cryptography;
using System.Text;
using MavericksBank.Contexts;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Repository;
using MavericksBank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace MavericksBankTest
{
    [TestFixture]
    public class AdminTest
	{
        RequestTrackerContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RequestTrackerContext>().UseInMemoryDatabase("dummy2Database").Options;
            context = new RequestTrackerContext(options);
        }

        [Test]
        [Order(1)]
        public async Task RegisterTest()
        {
            #region
            var _mockServicelogger = new Mock<ILogger<AdminService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockAdminlogger = new Mock<ILogger<AdminRepo>>();
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiuru");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);
            IRepository<Admin, int> _AdminRepo = new AdminRepo(_mockAdminlogger.Object, context);
            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IAdminService service = new AdminService(_mockServicelogger.Object,_CustRepo,_AdminRepo,_BankRepo,
                _BranchRepo,_BankEmpRepo,_UserRepo,_AccRepo,_TransacRepo,_BenifRepo,_LoanRepo,_LoanPolicyRepo, _TokenService);

            #endregion
            string name = "admin";
            string password = "admin";

            var result = await service.Register(name, password);

            Assert.IsNotNull(result);
        }

        [Test]
        [Order(2)]
        public async Task LoginTest()
        {
            #region
            var _mockServicelogger = new Mock<ILogger<AdminService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockAdminlogger = new Mock<ILogger<AdminRepo>>();
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiuru");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);
            IRepository<Admin, int> _AdminRepo = new AdminRepo(_mockAdminlogger.Object, context);
            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IAdminService service = new AdminService(_mockServicelogger.Object, _CustRepo, _AdminRepo, _BankRepo,
                _BranchRepo, _BankEmpRepo, _UserRepo, _AccRepo, _TransacRepo, _BenifRepo, _LoanRepo, _LoanPolicyRepo, _TokenService);

            #endregion
            string name = "admin";
            string password = "admin";

            var loginDTO = new LoginDTO
            {
                UserName = "admin",
                Password = "admin",
            };

            var result = await service.Login(loginDTO);

            Assert.IsNotNull(result);
        }

        [Test]
        [Order(3)]
        public async Task GetByIDTest()
        {
            #region
            var _mockServicelogger = new Mock<ILogger<AdminService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockAdminlogger = new Mock<ILogger<AdminRepo>>();
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiuru");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);
            IRepository<Admin, int> _AdminRepo = new AdminRepo(_mockAdminlogger.Object, context);
            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IAdminService service = new AdminService(_mockServicelogger.Object, _CustRepo, _AdminRepo, _BankRepo,
                _BranchRepo, _BankEmpRepo, _UserRepo, _AccRepo, _TransacRepo, _BenifRepo, _LoanRepo, _LoanPolicyRepo, _TokenService);

            #endregion

            var result = await service.GetAdminByID(1);

            Assert.IsNotNull(result);
        }

        [Test]
        [Order(4)]
        public async Task AddLoanPolicyTest()
        {
            #region
            var _mockServicelogger = new Mock<ILogger<AdminService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockAdminlogger = new Mock<ILogger<AdminRepo>>();
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiuru");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);
            IRepository<Admin, int> _AdminRepo = new AdminRepo(_mockAdminlogger.Object, context);
            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IAdminService service = new AdminService(_mockServicelogger.Object, _CustRepo, _AdminRepo, _BankRepo,
                _BranchRepo, _BankEmpRepo, _UserRepo, _AccRepo, _TransacRepo, _BenifRepo, _LoanRepo, _LoanPolicyRepo, _TokenService);

            #endregion


            var loanPolicy = new LoanPolicyDTO
            {
                LoanPolicyID = 1,
                LoanAmount = 20000,
                Interest = 10,
                TenureInMonths = 5
            };
            var result = service.AddLoanPolicies(loanPolicy);

            Assert.IsNotNull(result);
        }

        [Test]
        [Order(5)]
        public async Task UpdateLoanPolicyTest()
        {
            #region
            var _mockServicelogger = new Mock<ILogger<AdminService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockAdminlogger = new Mock<ILogger<AdminRepo>>();
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiuru");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);
            IRepository<Admin, int> _AdminRepo = new AdminRepo(_mockAdminlogger.Object, context);
            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IAdminService service = new AdminService(_mockServicelogger.Object, _CustRepo, _AdminRepo, _BankRepo,
                _BranchRepo, _BankEmpRepo, _UserRepo, _AccRepo, _TransacRepo, _BenifRepo, _LoanRepo, _LoanPolicyRepo, _TokenService);

            #endregion


            var loanPolicy = new LoanPolicies
            {
                LoanPolicyID = 1,
                LoanAmount = 20000,
                Interest = 10,
                TenureInMonths = 5
            };
            var result = service.UpdateLoanPolicies(loanPolicy);

            Assert.IsNotNull(result);
        }

        [Test]
        [Order(6)]
        public async Task DeleteLoanPolicyTest()
        {
            #region
            var _mockServicelogger = new Mock<ILogger<AdminService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockAdminlogger = new Mock<ILogger<AdminRepo>>();
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiuru");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);
            IRepository<Admin, int> _AdminRepo = new AdminRepo(_mockAdminlogger.Object, context);
            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IAdminService service = new AdminService(_mockServicelogger.Object, _CustRepo, _AdminRepo, _BankRepo,
                _BranchRepo, _BankEmpRepo, _UserRepo, _AccRepo, _TransacRepo, _BenifRepo, _LoanRepo, _LoanPolicyRepo, _TokenService);

            #endregion

            var result = service.DeleteLoanPolicies(1);

            Assert.IsNotNull(result);
        }

        [Test]
        [Order(7)]
        public async Task DeleteBeneficiaryTest()
        {
            #region
            var _mockServicelogger = new Mock<ILogger<AdminService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockAdminlogger = new Mock<ILogger<AdminRepo>>();
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiuru");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);
            IRepository<Admin, int> _AdminRepo = new AdminRepo(_mockAdminlogger.Object, context);
            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IAdminService service = new AdminService(_mockServicelogger.Object, _CustRepo, _AdminRepo, _BankRepo,
                _BranchRepo, _BankEmpRepo, _UserRepo, _AccRepo, _TransacRepo, _BenifRepo, _LoanRepo, _LoanPolicyRepo, _TokenService);

            #endregion

            var result = service.DeleteBeneficiary(1);

            Assert.IsNotNull(result);
        }


    }
}

