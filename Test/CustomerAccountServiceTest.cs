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
    public class CustomerAccountServiceTest
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
        public async Task OpenAccountTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);

            Accounts account = new Accounts();
            account.AccountNumber = 34567;
            account.AccountType = "Bussiness";
            account.Balance = 4000;
            account.CustomerID = 1;
            account.Status = "Approved";
            account.IFSCCode = "SBI1";
            await _AccRepo.Add(account);
            var accDTO = new AccountsCreateDTO();
            accDTO.AccountNumber = 0;
            accDTO.AccountType = "Savings";
            accDTO.Balance = 300;
            accDTO.customerID = 1;
            accDTO.IFSCCode = "SBI1";
            accDTO = await _AccService.OpenAccount(accDTO);

            Assert.IsNotNull(accDTO);

        }

        [Test]
        [Order(10)]
        public async Task CloseAccountTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);

            var account = await _AccService.CloseAccount(34567);
            Assert.That(account.AccountNumber == 34567);
            await _AccRepo.Delete(account.AccountNumber);
        }

        [Test]
        [Order(2)]
        public async Task EditAccountTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);


            var account = await _AccService.ViewAccountByID(34567);
            account.Status = "Approved";
            await _AccRepo.Update(account);
            Assert.That(account.Status == "Approved");
        }

        [Test]
        [Order(3)]
        public async Task GetAccountByIDTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);


            var account = await _AccService.ViewAccountByID(34567);
            Assert.That(account.AccountNumber == 34567);
        }

        [Test]
        [Order(5)]
        public async Task GetAllTransactionsTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);

            var acc = await _AccService.ViewAllYourAccounts(1);
            foreach (Accounts a in acc)
                Console.WriteLine(a.AccountNumber);
            var transacs = await _AccService.ViewAllYourTransactions(1);
            Assert.That(transacs.Count() == 1);
        }

        [Test]
        [Order(4)]
        public async Task AddTransactionTest()
        {
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);

            Transactions trans =
            new Transactions
            {
                TransactionDate = DateTime.Now,
                Amount = 3000,
                BeneficiaryAccountNumber = 12345,
                SAccountID = 34567,
                Status = "Successful",
                Description = "Self Deposit",
                TransactionID = 1,
                TransactionType = "Deposit"
            };


            _TransacRepo.Add(trans);
            var transactions = await _TransacRepo.GetAll();
            Assert.That(transactions.Count == 1);
        }

        [Order(6)]
        public async Task ViewLastNTransactionsTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);

            var acc = await _AccService.ViewAllYourAccounts(1);
            foreach (Accounts a in acc)
                Console.WriteLine(a.AccountNumber);
            var transacs = await _AccService.ViewAllYourTransactions(1);
            Assert.That(transacs.Count() == 1);
        }

        [Test]
        [Order(7)]
        public async Task ViewTransactionsByAccountTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);

            var transacs = await _AccService.ViewAllTransactionsByAccount(34567);
            Assert.That(transacs.Count() == 1);
        }

        [Test]
        [Order(8)]
        public async Task ViewTransactionsToAccountTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);

            var transacs = await _AccService.ViewAllTransactionsMadeToAnAccount(12345,1);
            Assert.That(transacs.Count() == 1);
        }

        [Test]
        [Order(8)]
        public async Task ViewTransactionsThisMonthTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);

            var transacs = await _AccService.ViewAllTransactionsInThisMonth(1);
            Assert.That(transacs.Count() == 1);
        }

        [Test]
        [Order(9)]
        public async Task ViewTransactionsLastMonthTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);

            var transacs = await _AccService.ViewAllTransactionsInTheLastMonth(1);
            Assert.That(transacs.Count() == 0);
        }

        [Test]
        [Order(9)]
        public async Task ViewAllBenifAccountsTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);

            var transacs = await _AccService.ViewAllYourBenifAccounts(1);
            Assert.That(transacs.Count() == 0);
        }

        [Test]
        [Order(9)]
        public async Task ViewAllBanksTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);

            var bank = new Banks();
            bank.BankID = 3;
            bank.BankName = "HDFC";

            await _BankRepo.Add(bank);
            var banks = await _AccService.ViewAllBanks();
            Assert.That(banks.Count() == 4);
        }

        [Test]
        [Order(9)]
        public async Task ViewAllBranchesTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerAccountService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);

            ICustomerAccountService _AccService = new CustomerAccountService(_mockServicelogger.Object, _TransacRepo, _AccRepo, _BenifRepo, _BankRepo, _BranchRepo);


            var branch = new Branches();
            branch.BankID = 3;
            branch.BranchName = "Gachibowli Branch";
            branch.City = "Hyderabad";
            branch.IFSCCode = "HDFC3";

            await _BranchRepo.Add(branch);
            var banks = await _AccService.ViewBankBranches(2);
            Assert.That(banks.Count() == 1);
        }
    }
}

