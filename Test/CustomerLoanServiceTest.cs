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

            var loan = new LoanApplyDTO();
            loan.CustomerID = 1;
            loan.LoanAmount = 2000;
            loan.LoanPolicyID = 1;
            loan.LoanPurpose = "House";

            var appliedLoan = await service.ApplyForALoan(loan);

            var loan2 = await service.GetAllAppliedLoans(loan.CustomerID);
            var loan3 = loan2.SingleOrDefault();
            loan3.Status = "Approved";
            await _LoanRepo.Update(loan3);
            Console.WriteLine(loan3.Status);
            Assert.That(appliedLoan.CustomerID == loan.CustomerID);

        }

        [Test]
        [Order(3)]
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

            var loan = new LoanExtendDTO();
            loan.LoanID = 1;
            loan.TenureInMonths = 5;
            loan = await service.AskForExtension(loan);

            Assert.That(loan.TenureInMonths == 5);
        }
        [Test]
        [Order(4)]
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
            //foreach(Loan l in loans)
            //    Console.WriteLine(l.LoanID+" "+l.Status);
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

            var loan = await service.RepayLoan(1, 123345, 2000);
            Assert.That(loan.Status == "Repayed");
        }


    }
}

