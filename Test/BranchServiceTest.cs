using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MavericksBank.Contexts;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Repository;
using MavericksBank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MavericksBankTest
{
    [TestFixture]
    public class BranchServiceTest
    {
        RequestTrackerContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RequestTrackerContext>().UseInMemoryDatabase("dummy2Database").Options;
            context = new RequestTrackerContext(options);
        }

        [Test]
        public async Task AddBranchTest()
        {
            var _mockServicelogger = new Mock<ILogger<BranchService>>();
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

            IBranchAdminService service = new BranchService(_mockServicelogger.Object, _BranchRepo);
            // Arrange
            var bank = new Banks();
            bank.BankID = 2;
            bank.BankName = "HDFC";

            await _BankRepo.Add(bank);

            var branchCreateDTO = new BranchCreateDTO
            {
                BankID = 2,
                BranchName = "Gachibowli Branch",
                IFSCCode = "ICICI123",
            };

            // Act
            var addedBranch = await service.AddBranch(branchCreateDTO);

            // Assert
            Assert.IsNotNull(addedBranch);

        }

        [Test]
        public async Task GetAllBranchTest()
        {
            var _mockServicelogger = new Mock<ILogger<BranchService>>();
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

            IBranchAdminService service = new BranchService(_mockServicelogger.Object, _BranchRepo);
            var branches = await service.GetAllBranches();

            Assert.That(branches.Count()==1);

        }

        [Test]
        public async Task GetBranchByIDTest()
        {
            var _mockServicelogger = new Mock<ILogger<BranchService>>();
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

            IBranchAdminService service = new BranchService(_mockServicelogger.Object, _BranchRepo);
            var branch = await service.GetBranchbyID("ICICI123");

            Assert.IsNotNull(branch);

        }

        [Test]
        public async Task UpdateBranchTest()
        {
            var _mockServicelogger = new Mock<ILogger<BranchService>>();
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

            IBranchAdminService service = new BranchService(_mockServicelogger.Object, _BranchRepo);

            var updatedBranch = new BranchUpdateDTO();
            updatedBranch.BankID = 2;
            updatedBranch.BranchName = "Madhapur Branch";
            updatedBranch.City = "Hyderabad";
            updatedBranch.IFSCCode = "ICICI123";

            var update = await service.UpdateBranch(updatedBranch);
            Assert.That(update.BranchName == "Madhapur Branch");


        }

        [Test]
        [Order(10)]
        public async Task  DeleteBranchTest()
        {
            var _mockServicelogger = new Mock<ILogger<BranchService>>();
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

            IBranchAdminService service = new BranchService(_mockServicelogger.Object, _BranchRepo);

            var branchCreateDTO = new BranchCreateDTO
            {
                BankID = 2,
                BranchName = "Gachibowli Branch",
                IFSCCode = "ICICI",
            };

            // Act
            var addedBranch = await service.AddBranch(branchCreateDTO);

            var branch = await service.DeleteBranch("ICICI");
            Assert.IsNotNull(branch);


        }

    }
}
