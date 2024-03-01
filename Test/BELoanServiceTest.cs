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
    public class BELoanServiceTest
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

            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service = new CustomerLoanService(_mockServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);
            var loanPolicy = new LoanPolicies()
            {
                LoanAmount = 2000,
                LoanPolicyID = 6,
                Interest = 10,
                TenureInMonths = 3
            };
            await _LoanPolicyRepo.Add(loanPolicy);

            var loan = new LoanApplyDTO();
            loan.CustomerID = 1;
            loan.LoanAmount = 2000;
            loan.LoanPolicyID = 1;
            loan.LoanPurpose = "House";
            var appliedLoan = await service.ApplyForALoan(loan);
            Console.WriteLine("LLLLL");
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

            Assert.That(appliedLoan.CustomerID== loan.CustomerID);

        }

        [Test]
        [Order(2)]
        public async Task ApproveLoanExtendTest()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<BankEmpLoanService>>();
            var _mockCustomerLoanServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockCustomerlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Customer, int> _CustomerRepo = new CustomerRepo(_mockCustomerlogger.Object, context);


            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service1 = new CustomerLoanService(_mockCustomerLoanServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);
            IBankEmpLoanService service2 = new BankEmpLoanService(_mockServicelogger.Object, _LoanRepo, _AccService,_CustomerRepo,service1,_LoanPolicyRepo);

            var loan = await service2.ApproveOrDisapproveLoanExtend(33, "Approve");
            Assert.That(loan.Status=="Deposited");


        }

        [Test]
        [Order(3)]
        public async Task GetAllLoansAppliedTest()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<BankEmpLoanService>>();
            var _mockCustomerLoanServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockCustomerlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Customer, int> _CustomerRepo = new CustomerRepo(_mockCustomerlogger.Object, context);


            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service1 = new CustomerLoanService(_mockCustomerLoanServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);
            IBankEmpLoanService service2 = new BankEmpLoanService(_mockServicelogger.Object, _LoanRepo, _AccService, _CustomerRepo, service1, _LoanPolicyRepo);

            var loan = await service2.GetAllLoansApplied();
            Assert.That(loan.Count == 3);


        }

        [Test]
        [Order(4)]
        public async Task GetAllLoansPoliciesTest()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<BankEmpLoanService>>();
            var _mockCustomerLoanServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockCustomerlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Customer, int> _CustomerRepo = new CustomerRepo(_mockCustomerlogger.Object, context);


            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service1 = new CustomerLoanService(_mockCustomerLoanServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);
            IBankEmpLoanService service2 = new BankEmpLoanService(_mockServicelogger.Object, _LoanRepo, _AccService, _CustomerRepo, service1, _LoanPolicyRepo);

            var loan = await service2.GetDifferentLoanPolicies();
            Assert.That(loan.Count == 2);


        }

        [Test]
        [Order(3)]
        public async Task GetAllLoansAppliedByCustomerTest()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<BankEmpLoanService>>();
            var _mockCustomerLoanServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockCustomerlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Customer, int> _CustomerRepo = new CustomerRepo(_mockCustomerlogger.Object, context);


            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service1 = new CustomerLoanService(_mockCustomerLoanServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);
            IBankEmpLoanService service2 = new BankEmpLoanService(_mockServicelogger.Object, _LoanRepo, _AccService, _CustomerRepo, service1, _LoanPolicyRepo);

            var loan = await service2.GetAllLoansAppliedByACustomer(1);
            Assert.That(loan.Count == 3);


        }

        [Test]
        [Order(3)]
        public async Task GetAllLoansThatNeedApprovalTest()
        {
            var _mockLoanlogger = new Mock<ILogger<LoanRepo>>();
            var _mockServicelogger = new Mock<ILogger<BankEmpLoanService>>();
            var _mockCustomerLoanServicelogger = new Mock<ILogger<CustomerLoanService>>();
            var _mockAccServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockLoanPolicylogger = new Mock<ILogger<LoanPoliciesRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockCustomerlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();

            IRepository<Loan, int> _LoanRepo = new LoanRepo(_mockLoanlogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<LoanPolicies, int> _LoanPolicyRepo = new LoanPoliciesRepo(_mockLoanPolicylogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Customer, int> _CustomerRepo = new CustomerRepo(_mockCustomerlogger.Object, context);


            ICustomerAccountService _AccService = new CustomerAccountService(_mockAccServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);
            ICustomerLoanService service1 = new CustomerLoanService(_mockCustomerLoanServicelogger.Object, _LoanRepo, _LoanPolicyRepo, _AccService, _AccRepo, _TransacRepo);
            IBankEmpLoanService service2 = new BankEmpLoanService(_mockServicelogger.Object, _LoanRepo, _AccService, _CustomerRepo, service1, _LoanPolicyRepo);

            var loan = await service2.GetAllLoansThatNeedApproval();
            Assert.That(loan.Count == 2);


        }
    }
}

