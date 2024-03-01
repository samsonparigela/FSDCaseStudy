using System;
using MavericksBank.Interfaces;
using MavericksBank.Models;using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MavericksBank.Services;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBankTest
{
    [TestFixture]
    public class BankServiceTest
    {
        private BankService _bankService;
        private Mock<IRepository<Banks, int>> _bankRepoMock;
        private Mock<ILogger<BankService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _bankRepoMock = new Mock<IRepository<Banks, int>>();
            _loggerMock = new Mock<ILogger<BankService>>();

            _bankService = new BankService(
                _loggerMock.Object,
                _bankRepoMock.Object
            );
        }

        [Test]
        public async Task AddBankTest()
        {
            // Arrange
            var bankCreateDTO = new BankCreateDTO
            {
                BankName="ICICI"
            };
            var bank = await _bankService.AddBank(bankCreateDTO);

            // Assert
            Assert.IsNotNull(bank.BankName=="ICICI");
        }

        [Test]
        public async Task DeleteBankTest()
        {
            // Arrange
            var banks = new Banks
            {
                BankID = 3,
                BankName = "ICICI"
            };

            // Setup mock behavior for GetByID method to return the bank
            _bankRepoMock.Setup(repo => repo.GetByID(banks.BankID)).ReturnsAsync(banks);

            // Setup mock behavior for Delete method to return the deleted bank
            _bankRepoMock.Setup(repo => repo.Delete(banks.BankID)).ReturnsAsync(banks);

            // Act
            var deletedBank = await _bankService.DeleteBank(banks.BankID);

            // Assert
            Assert.IsNotNull(deletedBank); // Check if deletedBank is not null, indicating successful deletion
            Assert.AreEqual(banks.BankID, deletedBank.BankID); // Check if the deletedBank ID matches the ID of the bank we attempted to delete
        }


        [Test]
        public async Task GetBanksByIDTest()
        {
            // Arrange
            var bank = new Banks { BankID = 1, BankName = "HDFC" };

            _bankRepoMock.Setup(repo => repo.GetByID(bank.BankID)).ReturnsAsync(bank);

            // Act
            var bank2 = await _bankService.GetBankbyID(bank.BankID);

            // Assert
            Assert.That(bank.BankID == bank2.BankID);
        }

        [Test]
        public async Task GetAllBanksTest()
        {
            // Arrange
            var expectedBanks = new List<Banks>
            {
                new Banks { BankID=1,BankName="HDFC" },
                new Banks { BankID=2,BankName="ICICI" },
                // Add more banks as needed
            };

            _bankRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedBanks);

            // Act
            var result = await _bankService.GetAllBanks();

            // Assert
            Assert.AreEqual(expectedBanks.Count, result.Count);
        }

        [Test]
        public async Task UpdateBankTest()
        {
            // Arrange
            var bankToUpdate = new Banks
            {
                BankID = 3,
                BankName = "ICICI"
            };

            var updatedBankInfo = new BankUpdateDTO
            {
                ID = bankToUpdate.BankID,
                BankName = "Updated ICICI"
            };

            _bankRepoMock.Setup(repo => repo.GetByID(bankToUpdate.BankID)).ReturnsAsync(bankToUpdate);

            _bankRepoMock.Setup(repo => repo.Update(It.IsAny<Banks>())).ReturnsAsync(bankToUpdate);

            // Act
            var updatedBank = await _bankService.UpdateBank(updatedBankInfo);

            // Assert
            Assert.IsNotNull(updatedBank);
            Assert.AreEqual(updatedBankInfo.ID, updatedBank.ID);
            Assert.AreEqual(updatedBankInfo.BankName, updatedBank.BankName);
        }


    }
}
