using System;
using MavericksBank.Contexts;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Repository;
using MavericksBank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace MavericksBankTest
{
	public class CustomerLoanServiceTest
	{
        RequestTrackerContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RequestTrackerContext>().UseInMemoryDatabase("dummy2Database").Options;
            context = new RequestTrackerContext(options);
        }

        [Test]
        [Order(2)]
        public async Task ApplyForALoanTest()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object,_TransacRepo,_AccRepo,_BenifRepo,_BankRepo,_BranchRepo);
            ICustomerLoanService service = new CustomerLoanService(_mockServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService,_AccRepo,_TransacRepo);
            var loanPolicy = new LoanPolicies()
            {
                LoanAmount = 2000,
                LoanPolicyID = 2,
                Interest = 10,
                TenureInMonths = 3
            };
            await _LoanPolicyRepo.Add(loanPolicy);
            var loan2 = new Loan()
            {
                LoanID = 333,
                LoanAmount = 1000,
                LoanPolicyID = 1,
                LoanPurpose = "Education",
                CustomerID = 1,
                CalculateFinalAmount = 1100,
                Status = "Deposited",
                TenureInMonths = 3

            };

            var loan3 = await _LoanRepo.Add(loan2);
            Assert.That(loan3.CustomerID == loan2.CustomerID);
            await _LoanRepo.Delete(loan3.LoanID);

        }

        [Test]
        [Order(4)]
        public async Task AskForExtensionTest()
        {

            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service = new CustomerLoanService(_mockServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);

            var loan3 = new Loan()
            {
                LoanID = 3334,
                LoanAmount = 1000,
                LoanPolicyID = 6,
                LoanPurpose = "Education",
                CustomerID = 2,
                CalculateFinalAmount = 1100,
                Status = "Pending",
                TenureInMonths = 3

            };
            var appliedLoan2 = await _LoanRepo.Add(loan3);

            var loan2 = new LoanExtendDTO();
            loan2.LoanID = 3334;
            loan2.TenureInMonths = 5;
            loan2.Status = "Extend Request";

            loan2 = await service.AskForExtension(loan2);

            Assert.IsNotNull(loan2);


        }
        [Test]
        [Order(3)]
        public async Task GetAllAppliedLoansTest()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service = new CustomerLoanService(_mockServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);

            var loans = await service.GetAllAppliedLoans(1);
            Assert.That(loans.Count() == 1);
        }
        [Test]
        [Order(5)]
        public async Task GetAllAvailedLoansTest()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service = new CustomerLoanService(_mockServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);


            var loans = await service.GetAllAvailedLoans(1);
            Assert.That(loans.Count() == 0);
        }
        [Test]
        [Order(6)]
        public async Task GetLoanByID()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service = new CustomerLoanService(_mockServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);

            var loan = await service.GetLoanByID(1);
            loan.Status = "Approved";
            await _LoanRepo.Update(loan);
            Assert.That(loan.LoanID == 1);
        }

        [Test]
        [Order(1)]
        public async Task AddAccountTest()
        {
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);

            Accounts account = new Accounts
            {

                AccountNumber = 123345,
                CustomerID = 1,
                Status = "Approved",
                AccountType = "Savings",
                Balance = 5000,
                IFSCCode = "SBI1",
            };

            account = await _AccRepo.Add(account);
            context.SaveChanges();
            Assert.That(account.AccountNumber == 123345);


        }

        [Test]
        [Order(7)]
        public async Task GetLoanAmountToAccountTest()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service = new CustomerLoanService(_mockServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);

            var account = await service.GetLoanAmountToAccount(1, 123345);
            Console.WriteLine(account.Balance);
            Assert.That(account.Balance==7000);
        }
        [Test]
        [Order(8)]
        public async Task RepayLoanTest()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service = new CustomerLoanService(_mockServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);

            var loan = await service.RepayLoan(1, 123345, 2200);
            Assert.That(loan.Status == "Repayed");
        }


    }
}

