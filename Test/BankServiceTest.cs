using System;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MavericksBank.Services;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Contexts;
using Microsoft.EntityFrameworkCore;
using MavericksBank.Repository;

namespace MavericksBankTest
{
    [TestFixture]
    public class BankServiceTest
    {
        RequestTrackerContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RequestTrackerContext>().UseInMemoryDatabase("dummy2Database").Options;
            context = new RequestTrackerContext(options);
        }

        [Test]
        public async Task AddBankTest()
        {
            var _mockServicelogger = new Mock<ILogger<BankService>>();
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

            IBankAdminService service = new BankService(_mockServicelogger.Object, _BankRepo);
            // Arrange
            var bank = new Banks();
            bank.BankID = 4;
            bank.BankName = "HDFC";

            await _BankRepo.Add(bank);

            var bankDTO = new BankCreateDTO();
            bankDTO.BankName = "Union Bank";

            var addedBank = await service.AddBank(bankDTO);

            // Assert
            Assert.IsNotNull(bankDTO);

        }

        [Test]
        [Order(10)]
        public async Task DeleteBankTest()
        {
            var _mockServicelogger = new Mock<ILogger<BankService>>();
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

            IBankAdminService service = new BankService(_mockServicelogger.Object, _BankRepo);

            var bank = new Banks();
            bank.BankID = 4;
            bank.BankName = "HDFC";

            await _BankRepo.Add(bank);

            var banks = await service.DeleteBank(4);

            // Assert
            Assert.IsNotNull(banks);

        }

        [Test]
        public async Task GetBankByIDTest()
        {
            var _mockServicelogger = new Mock<ILogger<BankService>>();
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

            IBankAdminService service = new BankService(_mockServicelogger.Object, _BankRepo);

            var addedBank = await service.GetBankbyID(4);

            // Assert
            Assert.IsNotNull(addedBank);

        }

        [Test]
        public async Task GetAllBanksTest()
        {
            var _mockServicelogger = new Mock<ILogger<BankService>>();
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

            IBankAdminService service = new BankService(_mockServicelogger.Object, _BankRepo);

            var banks = await service.GetAllBanks();

            // Assert
            Assert.That(banks.Count()==2);

        }

        [Test]
        public async Task UpdateBanksTest()
        {
            var _mockServicelogger = new Mock<ILogger<BankService>>();
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

            IBankAdminService service = new BankService(_mockServicelogger.Object, _BankRepo);

            var bankDTO = new BankUpdateDTO();
            bankDTO.ID = 4;
            bankDTO.BankName = "City Bank";
            var bank = await service.UpdateBank(bankDTO);

            // Assert
            Assert.That(bank.BankName== "City Bank");

        }


    }
}
